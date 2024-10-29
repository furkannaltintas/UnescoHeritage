using System;
using System.IO;
using System.Collections;
using Program;


namespace Program{
    public class UMAlani{
        public int ilanYili;
        public String alanAdi;
        public String[] ilAdlari;
        public UMAlani(int ilanYili,String alanAdi,String[] ilAdlari){
            this.ilanYili = ilanYili;
            this.alanAdi = alanAdi;
            this.ilAdlari = ilAdlari;
        }
        public void  yazdir(){


            Console.WriteLine( "Alan Adı: " + alanAdi +
                  "\nİlan Yılı: " + ilanYili );
            Console.Write("Bulunduğu illerin adları: ");
            foreach(String s in ilAdlari){        
                Console.Write(s+", ");
                
            }
            Console.WriteLine();
        }
        
        
    }
    public class Program{
        public static void Main(String[] args ){
            List<String[]> bolgelerdekiIller = new List<String[]>();
            List<List<UMAlani>> bolgelerdekiAlanlar = new List<List<UMAlani>>();            // generic list oluşturuldu ve yazdırıldı.
            genericListOluştur(bolgelerdekiAlanlar,bolgelerdekiIller);
            genericListYazdir(bolgelerdekiAlanlar);

            List<UMAlani> tumAlanlar = new List<UMAlani>();

            // STACK,QUEUE OLUŞTURUP YAZDIRMA KISMI 

            Stack<UMAlani> UMStack = new Stack<UMAlani>();                  // Queue ve Stack yapıları oluşturuldu.
            Queue<UMAlani> UMQueue = new Queue<UMAlani>();

            PriorityQueue pQ = new PriorityQueue();               // Priority Queue oluşturuldu.
            List<String> mirasIsimleri = new List<String>();


            foreach (List<UMAlani> uM in bolgelerdekiAlanlar)
            {
                foreach(UMAlani u in uM){ 
                    bool kontrol = true;  
                    
                    for(int i = 0; i<mirasIsimleri.Count;i++){              // Oluşturduğumuz bolgelerdekiAlanlar listesinde 
                        if(u.alanAdi == mirasIsimleri[i]){                  // aynı veri birkaç kez tekrar ettiği için burada
                            kontrol = false;                                // kontrol ettirerek Stack'e aynı veriyi birden çok kez eklemesini
                        }                                                   // engelledik.
                    }
                    if(kontrol){
                        tumAlanlar.Add(u);
                        UMStack.Push(u);
                        UMQueue.Enqueue(u);
                    }
                    mirasIsimleri.Add(u.alanAdi);
                }
                
            }
            int Stack_Queue_boyutu = UMStack.Count;
            
            for (int j = 0;j<Stack_Queue_boyutu;j++){                             // Queue'dan verileri çıkartıp yazdırma işlemi.
                UMAlani yazdirilacak = UMQueue.Dequeue();
                yazdirilacak.yazdir();
            }
            
           for(int i = 0;i<Stack_Queue_boyutu;i++){
                UMAlani yazdirilacak = UMStack.Pop();                               // Stack'ten verileri çıkartıp yazdırma işlemi.
                yazdirilacak.yazdir();
                }

                // STACK ,QUEUE OLUŞTURUP YAZDIRMA KISMI SONU

            // PRIORITY QUEUE SINIFINDAN OBJE OLUŞTURUP İŞLEM YAPMA KISMI
            int liste_uzunlugu = tumAlanlar.Count;
            for(int k = 0;k< liste_uzunlugu;k++){
                pQ.ekle(tumAlanlar[k]);
            }
           for(int k = 0;k< liste_uzunlugu;k++){
                pQ.sil();
            }
        }
            
            

            

            
        
        public static void genericListOluştur(List<List<UMAlani>> bolgelerdekiAlanlar,List<String[]> bolgelerdekiIller){
            string line;
            string line2;
            int num = 0;
            
            
            for(int k = 0;k<7;k++){
                    bolgelerdekiAlanlar.Add(new List<UMAlani>());
                }
            string a = "/Users/furkan/Desktop/bolgedekiIller.txt";              // txt den veriler okundu.
            string b ="/Users/furkan/Desktop/belge_2.txt";
            StreamReader sr = new StreamReader(a);
            StreamReader sr1 = new StreamReader(b);
            
            line = sr.ReadLine();   
            while(line!=null){
                String[] iller = line.Split(", ") ;
                bolgelerdekiIller.Add(iller);
                line = sr.ReadLine();
            }
            line2 = sr1.ReadLine();
            while(line2!= null){
                String[] ucluVeri = line2.Split(",");
                                                                        // her bir line'daki üçlü veriyi split ile ayırdık.
                String[] ucluVeriSehir = ucluVeri[1].Split("-");        // ayrıca farklı iller ile birden çok bölgede bulunabilme
                                                                        // ihtimalinden dolayı, şehirleri içeren indisi de split ile    
                                                                        // döndürüp kontrollerini yaptık.
                for(int i = 0;i<7;i++){
                    foreach(String str in ucluVeriSehir){
                        
                        for(int j = 0; j< bolgelerdekiIller[i].Length; j ++)
                       
                        if(str == bolgelerdekiIller[i][j]){
                            bolgelerdekiAlanlar[i].Add(new UMAlani(int.Parse(ucluVeri[2]), ucluVeri[0],ucluVeriSehir));
                           
                            
                        }
                    }
                }
                line2 = sr1.ReadLine();

            }
            
        }
        public static void genericListYazdir(List<List<UMAlani>> bolgelerdekiAlanlar){
            String[] bolgeler = { "Akdeniz","Doğu Anadolu","Ege","GüneyDoğu Anadolu","İç Anadolu","Karadeniz","Marmara" };
            int num =0;
            foreach (List<UMAlani> uM in bolgelerdekiAlanlar)
            {
                Console.WriteLine(bolgeler[num] + " bolgesinde bulunan tarihi alan sayısı: "+ uM.Count);                // ekrana yazdırma metodu. 
                Console.WriteLine("Alanların özellikleri: ");
                foreach (UMAlani u in uM)
                {
                    u.yazdir();

                }
                Console.WriteLine();
                num++;
            }
        }
    }
    public class PriorityQueue{
        List<UMAlani> liste;
        public PriorityQueue(List<UMAlani> liste){                      // constructor oluşturuldu(boş ve parametreli)
                                                                        // liste adında bir generic List yapısı barındırıyor.
            this.liste = liste;
        }
        public PriorityQueue(){
            liste = new List<UMAlani>();
        }
        public void ekle(UMAlani u){
            liste.Add(u);
        }
        public void sil(){
            int silinecek_Index = 0;
            for(int i =0;i<liste.Count-1;i++){
                                                                                        // CompareTo metodu ile karşılaştırma yapıp
                if(liste[i].alanAdi.CompareTo(liste[silinecek_Index].alanAdi) < 0){     // ASCII koduna göre string leri kontrol ettik.
                    silinecek_Index = i;                                                //
                    }
                    
            }
        liste[silinecek_Index].yazdir();    
        liste.RemoveAt(silinecek_Index);    

        }
        public bool bosMu(){
            if(liste.Count == 0){
                return true;
            }
            else{
                return false;
            }
        }
    }
}