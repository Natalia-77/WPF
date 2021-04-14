using CatShop.Application;
using CatShop.Application.Interfaces;
using CatShop.Domain;
using CatShop.EFData;
using CatShop.Infrastructure.Service;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfCatShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int _id { get; set; }
        public int Maxim { get; set; }
        private EFContext _context = new EFContext();
        private ICatService _catService = new CatService();
        private ObservableCollection<CatVM> _cats = new ObservableCollection<CatVM>();
        //private static ManualResetEvent _manreset = new ManualResetEvent(false);
        //private BackgroundWorker worker = null;
        readonly ManualResetEvent _busy = new ManualResetEvent(false);

        public  MainWindow()
        {
            InitializeComponent();
            _catService.AddNewCatItem +=UpdateUI;
            //DBSeeder.SeedData(_context);
            //this.DataContext = new CatVM();
        }
                

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            new AddNewCat(_cats).ShowDialog();            
        }

       public async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //в строчці поточного стану завантаження виводимо повідомлення,відповідно до етапу виконання.
            tbstatus.Text = "Start load data...";
            //починаємо відлік часу для визначеного етапу.
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            //виконання задачі-ініціалізовуємо дані про котів.
            await Task.Run(() =>
            {
                _context.Cats.Count();
               
            });
            //зупинка вдліку часу на ініціалізацію.
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            //визначаємо час ,витрачений на ініціалізацію.
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
               ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            //в окремому тексбоксі виводимо час,відповідного етапу виконання задачі.
            tbtime.Text = elapsedTime;
            //pbsimple.Value=(_catService.Count()*100)/elapsedTime.Length;
            //в строчці поточного стану завантаження виводимо повідомлення,відповідно до етапу виконання.
            tbstatus.Text = "Connect to database... ";

            //виконання задачі-заповнення даними бази даних.
            await DBSeeder.SeedDataAsync(_context);

            // починаємо новий відлік часу для наступного етапу:отримання множини значень з бази даних.
            stopWatch = new Stopwatch();
            stopWatch.Start();
            var list = _context.Cats.AsQueryable()
               .Select(x => new CatVM()
               {
                   Id = x.Id.ToString(),
                   Name = x.Name,
                   Birthday = x.Birth,
                   Description = x.Description,
                   Gender = x.Gender,
                   Price = x.AppCatPrices.OrderByDescending(x => x.DateCreate).FirstOrDefault().Price,
                   Image = x.Image
                   
               }).ToList();
            //зупинка відліку часу на отримання множини значень.
            stopWatch.Stop();
            ts = stopWatch.Elapsed;          
            elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            //в окремому тексбоксі виводимо час,відповідного етапу виконання задачі.
            tbtime.Text = elapsedTime;
            //в строчці поточного стану завантаження виводимо повідомлення,відповідно до етапу виконання.
            tbstatus.Text = "Successul load database! ";
            //отриману множину значень з бази даних представляємо у вигляді колекції КетВьюМодел і виводимо в датагрід.
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
        #region Thread BackgroundWorker
        /// <summary>
        /// Add cats when you push a button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        //private void btncatsnew_Click(object sender, RoutedEventArgs e)
        //{

        //    btncatsnew.IsEnabled = false;
        //    btncancel.IsEnabled = true;
        //    btnpause.IsEnabled = true;
        //    pbsimple.Value = 0;
        //    worker = new BackgroundWorker();
        //    worker.WorkerReportsProgress = true;
        //    worker.WorkerSupportsCancellation = true;

        //    worker.DoWork+= worker_DoWork;
        //    worker.ProgressChanged+= worker_ProgressChanged;
        //    worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        //    StartWorker();
        //   // worker.RunWorkerAsync();
        //}

        //void worker_DoWork(object sender, DoWorkEventArgs e)
        //{

        //    //int max = (int)e.Argument;
        //    int max = 5;

        //    ICatService catService = new CatService();
        //    // catService.AddNewCatItem += UpdateUIAsync;
        //   //catService.AddCat();

        //    for (int i = 1; i <= max; i++)
        //    {
        //        _busy.WaitOne();
        //        if (worker.CancellationPending )
        //        {
        //            e.Cancel = true;
        //            break;
        //        }
        //        catService.AddCat();
        //        int progressPercentage = Convert.ToInt32(((double)i * 100)/max);              
        //        (sender as BackgroundWorker).ReportProgress(progressPercentage);
        //        Thread.Sleep(2000);

        //    }           


        //}
        //void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{            
        //   pbsimple.Value = e.ProgressPercentage;

        //}

        //void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    if (e.Cancelled)
        //    {                 

        //        tbstatus.Text = "Cancelled by user...";
        //    }
        //    else
        //    {

        //        tbstatus.Text= "Cats added";
        //    }
        //    btncatsnew.IsEnabled = true;
        //    btncancel.IsEnabled = false;
        //    btnpause.IsEnabled = false;

        //}

        //private void btncancel_Click(object sender, RoutedEventArgs e)
        //{
        //    if (worker.IsBusy)
        //    {
        //        worker.CancelAsync();
        //        _busy.Set();
        //        pbsimple.Value = 0;
        //    }

        //}

        //void StartWorker()
        //{
        //    //якщо не запущений процес на виконання,то запускаю.
        //    if (!worker.IsBusy)
        //    {
        //        worker.RunWorkerAsync();
        //    }
        //    // якщо запущений був раніше,але припинений паузою,то розблокoвую worker.
        //    _busy.Set();

        //}

        //private void btnpause_Click(object sender, RoutedEventArgs e)
        //{
        //    //якщо кнопка пауза не натиснута і ми її натискаємо.
        //    if(btnpause.Content.ToString()=="Pause")
        //    {
        //        //викликаю ПаузВоркер.
        //        PauseWorker();
        //        btnpause.Content = "Resume";
        //    }
        //    //в іншому випадку,кнопка Пауза натиснута,тому я натискаю її знову,щоб продовжити дію.
        //    else
        //    {
        //        //викликаю СтартВоркер.
        //        StartWorker();
        //        btnpause.Content = "Pause";
        //    }
        //}
        //void PauseWorker()
        //{
        //    // блокую виконання worker
        //    _busy.Reset();

        //}
        #endregion

        #region Async/await

        //Відображення зміни даних в UI.
         void UpdateUI(int i)
         {
            Dispatcher.Invoke(new Action(() =>
            {
                btncatsnew.Content = $"{i}";

                //в прогрессбарі відображається кількість елементів,відповідно до їх додавання.
                pbsimple.Value = i; 
                
            }));

         }       

        //обробник кнопки додавання даних в базу.
        public async void btncatsnew_Click(object sender, RoutedEventArgs e)
        {
            //виводимо дані про поточний потік.
            Debug.WriteLine("Thread id: {0}", Thread.CurrentThread.ManagedThreadId);

            //після натискання кнопка додавання перестає бути активною.
            btncatsnew.IsEnabled = false;
            _busy.Set();

            //кількість котів для додавання.
            int count = 3;
            pbsimple.Maximum = count;
            await _catService.InsertCatsAsync(count, _busy);   
         
        }


        //обробник кнопки паузи рід час задачі з додавання нових даних в базу.
        private void btnpause_Click(object sender, RoutedEventArgs e)
         {
            //якщо кнопка пауза не натиснута і ми її натискаємо.
            if (btnpause.Content.ToString() == "Pause")
            {      
                //блокування виконання.
                _busy.Reset();
                btnpause.Content = "Resume";
            }
            //в іншому випадку,кнопка Пауза натиснута,тому я натискаю її знову,щоб продовжити дію.
            else
            {
                //якщо запущений був раніше, але припинений паузою, то розблокoвую .
                _busy.Set();
                btnpause.Content = "Pause";
            }
        }

        //обробник кнопки остаточного скасування додавання даних.
        private void btncancel_Click(object sender, RoutedEventArgs e)
        {
            //властивість стає тру,відповідно до чого в КетСервіс додавання котів буде перервано.Відповідно і не записано в БД.
            _catService.IsCancelled = true;

            //кнопка стане неактивною.
            btncancel.IsEnabled = false;

            //кнопка ПАУЗА також стає неактивною.
            btnpause.IsEnabled = false;
        }
        #endregion


    }
}

