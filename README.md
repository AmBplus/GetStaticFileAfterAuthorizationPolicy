


سلام دوستان من امیر معصوم بیگی هستم تو این مقاله میخوام در مورد اینکه چطور میتونیم یک فایل رو asp.net core بعد از احراز هویت ارسال کنیم صحبت کنم .
 اگه شما هم تا الان تلاش کرده باشید که به فایل های wwwroot دسترسی پیدا کنین ، میبینین که این کار به راحتی با وارد کردن آدرس url در بالای صفحه اتفاق میفته ، مثلا شما آدرس زیر رو مینویسین
https://localhost:7008/ANIMAL.JPG
بعد میبینین که به راحتی به این آدرس دسترسی دارین و فایل رو دریافت میکنین ، واسه یه سری سناریو ها اینکار چیز خوبیه ، ولی اگر یک وقت شما خواستید در صورتی که مثلا کاربرتون لاگین کرده بود فایل رو ارسال کنین ، اون وقت باید چیکار کنین ؟ 
اول از همه لینک گیت هاب این پروژه به آدرس زیر که اگه این مقاله واستون مفید بود میتونید بهش ستاره هم بدید 😊 :


GitHub - AmBplus/GetStaticFileAfterAuthorizationPolicy: Get Static File After AuthorizationPolicy in asp.net core
GitHub - AmBplus/GetStaticFileAfterAuthorizationPolicy: Get Static File After AuthorizationPolicy in asp.net core
Get Static File After AuthorizationPolicy in asp.net core  - GitHub - AmBplus/GetStaticFileAfterAuthorizationPolicy: Get Static File After AuthorizationPolicy in asp.net core
github.com
نکته اول اینکه که هر فایلی که تو مسیر  wwwroot میزارید  در صورتی که در middleware استاتیک فایل هارو ادد کرده باشید اگر آدرس فایل رو وارد کنید اون رو به شما نشون میده ، میدل ور زیر :

app.UseStaticFiles();

اما خیلی وقتا ما نیاز داریم که به استاتیک فایل ها در wwwroot دسترسی داشته باشیم ، پس نمیتونیم در اکثر مواقع این میدلور رو حذف کنیم ، و این میدلور قبل از میدل ور های زیر که مربوط به احراز هویت هستن قرار میگیره :

app.UseAuthentication();

app.UseAuthorization();

راه حل اینکار که سایت ماکروسافت هم ارائه داده ، اینه که ما یک پوشه دیگه در برنامه بسازیم ، بجز wwwroot و اینجور فایل هارو در اون مسیر قرار بدیم ، مثل عکس زیر که ما پوشه MyStaticFiles رو ایجاد کردیم و داخل اون یک پوشه به اسم images و درونش یک عکس گل قرار دادیم که قراره این عکس رو بعد از اینکه لاگین کردیم ببینیم ، اگه وارد لینک گیت هاب بشید میتونید سورس رو به طور کامل ببنید .



حالا نیازه یه سری تنظیمات در فایل program.cs ایجاد کنیم ، برای منظور بعد از میدل ور های احراز هویت و قبل از میدل ور هایی که کنترلر یا صفحات ریزور رو شناسایی میکنه ما کد زیر رو قرار میدیم

app.UseStaticFiles(new StaticFileOptions{
FileProvider = new PhysicalFileProvider(
    Path.Combine(builder.Environment.ContentRootPath, "MyStaticFiles")),
RequestPath = "/StaticFiles"});



کد بالا یک میدلور هست که پوشه mystaticfiles که ما ساختیم رو به برنامه معرفی میکنه ، و ما حالا از داخل برنامه میتونیم به اون دسترسی داشته باشیم ، تفاوتی که با پوشه wwwroot در حالت عادی داره اینه که اگر آدرس فایل های داخل این پوشه رو از url وارد کنیم ما چیزی رو نمیتونیم دریافت کنیم که هدف اصلی ما هم همینه .
کل کد در قسمت program.cs به صورت زیره ، از اونجایی که واسه تست این برنامه ما میخواستیم احراز هویت رو هم انجام بدیم یه سری کد اضاف برای این منظور هم قرار گرفته 


برای اینکه ببینیم کاری که کردیم چطور میشه ازش استفاده کرد به صفحه GetFileWithAuth میریم که در آدرس زیر قرار داره
https://localhost:7008/GetFileWithAuth 
اگر بدون اینکه لاگین کرده باشید وارد این صفحه بشید ، شما به صفحه لاگین هدایت میشید و هیچیزی به شما نمایش داده نمیشه ، ولی اگر لاگین کرده باشید ، میتونید به این صفحه دسترسی داشته باشید و در متد Get این صفحه عکس یک گل که در آدرس زیر قرار داره برای شما ارسال میشه
MyStaticFiles/Images/flower.jpg
اگر این آدرس رو به صورت زیر وارد کنین هیچ چیزی به شما نمایش داده نمیشه
https://localhost:7008/MyStaticFiles/Images/flower.jpg 
یا
https://localhost:7008/Images/flower.jpg
که این بر خلاف حالت قبل که در wwwroot فایل قرار میدادیم هست ، در حالت قبل اگر مسیر بعد از wwwroot رو مینوشتیم فایل برای ما ارسال میشد ، در حالت این اتفاق نمیفته ، و الان اگر بخوایم به فایل دسترسی داشته باشیم میتونیم به صورت زیر عمل کنیم ، در اینجا به طور مثال یک صفحه ایجاد کردیم و در اون صفحه بعد از احراز هویت فایل رو به کاربر نشون میدیم
سورس این صفحه به صورت زیره که در لینک گیت هاب پروژه که بالا فرستادم هم میتونید در پوشه pages اون رو مشاهده کنید .
همونجوری که میبنید ما اتربیتوت [Authorize] رو در بالای صفحه گزاشتیم ، اگر با ریزور پیج آشنایی داشته باشید میدونید که ما نمیتونیم این اتریبوت رو بالای هندرلر ها بزاریم ، اگر در کنترلر میخواید استفاده کنید میتونید این اتربیوت رو بالای اکشن بزارید ، اما در قسمت OnGet همونجوری که میبینید ما یک نتیجه یک فایل فیزیکی یا همون PhysicalFileResult رو برمیگردونیم ، که  بعد از به دست آوردن مسیر ریشه پروژه و ساختن مسیر فایل ما اون رو به صورت یک PhysicalFile به راحتی برمیگردونیم ، که البته این فایل بعد از احراز هویت به کاربر برگردونده شده و ارسال فایل کاملا در اختیار ما قرار داره 
به طور خلاصه برای اینکه بخواید یک فایل رو فقط سرور خودتون برگردونه کار های زیر رو میکنید
1. ایجاد یک پوشه در مسیر ریشه پروژه
2. شناساندن مسیر ایجاد پوشه بعد از میدل ور های احراز هویت و بررسی دسترسی
3. ارسال فایل از درون برنامه به صورت دستی و نه به صورت url بعد از چک کردن هایی که نیاز دارید .
      *امیدوارم که این مقاله برای شما مفید بوده باشه ، در صورتی که ایرادی دیدید بگید اصلاح کنم .
