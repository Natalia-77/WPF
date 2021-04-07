using CatShop.Application;
using CatShop.Application.Interfaces;
using CatShop.Domain;
using CatShop.EFData;
using CatShop.Infrastructure.Service;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows;

namespace WpfCatShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int _id { get; set; }
        private EFContext _context = new EFContext();
        private ObservableCollection<CatVM> _cats = new ObservableCollection<CatVM>();
        private BackgroundWorker worker = null;
        readonly ManualResetEvent _busy = new ManualResetEvent(false);

        public MainWindow()
        {
            InitializeComponent();
            DBSeeder.SeedData(_context);
            //this.DataContext = new CatVM();
        }

        private void Cat_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            new AddNewCat(_cats).ShowDialog();            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var list = _context.Cats
               .Select(x => new CatVM()
               {
                   Id=x.Id.ToString(),                   
                   Name = x.Name,
                   Birthday = x.Birth,
                   Description = x.Description,
                   Gender=x.Gender,
                   Price=x.AppCatPrices.OrderByDescending(x=>x.DateCreate).FirstOrDefault().Price,
                   Image=x.Image
               }).ToList();

          
            _cats = new ObservableCollection<CatVM>(list);            
            dgSimple.ItemsSource = _cats;

        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            Window_Loaded(sender, e);
        }

        private void btnChangeUser_Click(object sender, RoutedEventArgs e)
        {
                      
            if (dgSimple.SelectedItem != null)
            {
                if (dgSimple.SelectedItem is CatVM)
                {
                    var userView = dgSimple.SelectedItem as CatVM;                  
                    int id =int.Parse( userView.Id);
                    _id = id;                   
                }               
            }

            EditCat edit = new EditCat(_id);
            edit.ShowDialog();
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (dgSimple.SelectedItem != null)
            {
                if (dgSimple.SelectedItem is CatVM)
                {
                    var userView = dgSimple.SelectedItem as CatVM;
                    int id = int.Parse(userView.Id);
                    _id = id;
                    var cat = _context.Cats.SingleOrDefault(c => c.Id == id);
                    _context.Cats.Remove(cat);
                    _context.SaveChanges();
                }
            }
           
        }
        /// <summary>
        /// Add cats when you push a button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btncatsnew_Click(object sender, RoutedEventArgs e)
        {
            ////Debug.WriteLine("Thread id: {0}", Thread.CurrentThread.ManagedThreadId);
            ////btnAddRange.IsEnabled = false;
            ////ShowMessage();
            ///
            //======
            //Thread thread = new Thread(Adde);
            //thread.Start();
            //=====

            pbsimple.Value = 0;
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
           
            worker.DoWork+= worker_DoWork;
            worker.ProgressChanged+= worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            StartWorker();
           // worker.RunWorkerAsync();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {

            //int max = (int)e.Argument;
            int max = 5;
         
            ICatService catService = new CatService();
            // catService.AddNewCatItem += UpdateUIAsync;
           //catService.AddCat();
            
            for (int i = 1; i <= max; i++)
            {
                _busy.WaitOne();
                if (worker.CancellationPending )
                {
                    e.Cancel = true;
                    break;
                }
                catService.AddCat();
                int progressPercentage = Convert.ToInt32(((double)i * 100)/max);              
                (sender as BackgroundWorker).ReportProgress(progressPercentage);
                Thread.Sleep(2000);
               
            }           


        }
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {            
           pbsimple.Value = e.ProgressPercentage;

        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {                 
               
                tbstatus.Text = "Cancelled by user...";
            }
            else
            {
               
                tbstatus.Text= "Cats added";
            }
           
        }

        private void btncancel_Click(object sender, RoutedEventArgs e)
        {
            if (worker.IsBusy)
            {
                worker.CancelAsync();
                _busy.Set();
                pbsimple.Value = 0;
            }
           
        }

        void StartWorker()
        {
            //якщо не запущений процес на виконання,то запускаю.
            if (!worker.IsBusy)
            {
                worker.RunWorkerAsync();
            }
            // якщо запущений був раніше,але припинений паузою,то розблокoвую worker.
            _busy.Set();
          
        }

        private void btnpause_Click(object sender, RoutedEventArgs e)
        {
            //якщо кнопка пауза не натиснута і ми її натискаємо.
            if(btnpause.Content.ToString()=="Pause")
            {
                //викликаю ПаузВоркер.
                PauseWorker();
                btnpause.Content = "Resume";
            }
            //в іншому випадку,кнопка Пауза натиснута,тому я натискаю її знову,щоб продовжити дію.
            else
            {
                //викликаю СтартВоркер.
                StartWorker();
                btnpause.Content = "Pause";
            }
        }
        void PauseWorker()
        {
            // блокую виконання worker
            _busy.Reset();
            
        }


        //private void Adde()
        //{
        //    Dispatcher.Invoke(new Action(() =>
        //    {
        //        btncatsnew.IsEnabled = false;
        //    }));
        //    ICatService catService = new CatService();
        //    catService.AddNewCatItem += UpdateUIAsync;
        //    catService.AddCat(5);

        //}

        //void UpdateUIAsync(int i)
        //{
        //    Dispatcher.Invoke(new Action(() =>
        //    {
        //       btncatsnew.Content = $"{i}";
        //        Debug.WriteLine("Thread id: {0}", Thread.CurrentThread.ManagedThreadId);
        //    }));

        //}
    }
}

