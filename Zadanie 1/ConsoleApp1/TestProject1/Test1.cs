using ConsoleApp1;

namespace TestProject1
{
    [TestClass]
    public sealed class Test1
    {
        private Class1 _class1;

        [TestInitialize]
        public void Setup()
        {
            _class1 = new Class1();
        }

        [TestMethod]
        public void Potega_PrawidloweDane_ZwracaPoprawnyWynik()
        {
            string result = _class1.Potega(2, 3, 1);
            Assert.AreEqual("8", result);
        }

        [TestMethod]
        public void Potega_UjemnyWykladnik_ZwracaKomunikatOBledzie()
        {
            string result = _class1.Potega(2, -3, 1);
            Assert.AreEqual("wykladnik mniejszy od 0", result);
        }

        [TestMethod]
        public void Potega_UjemneC_ZwracaKomunikatOBledzie()
        {
            string result = _class1.Potega(2, 3, -1);
            Assert.AreEqual("trzeci argument jest mniejszy od 0", result);
        }

        [TestMethod]
        public void ZapiszWTablicy_PrawidloweDane_ZwracaPoprawnaTablice()
        {
            var result = _class1.ZapiszWTablicy(new int[] { 1, 2, 3 }, 2);
            CollectionAssert.AreEqual(new int[] { 2, 4, 6 }, result);
        }

        [TestMethod]
        public void ZapiszWTablicy_PustaTablica_ZwracaPustaTablice()
        {
            var result = _class1.ZapiszWTablicy(new int[] { }, 2);
            CollectionAssert.AreEqual(new int[] { }, result);
        }

        [TestMethod]
        public void PoleKola_PrawidloweDane_ZwracaPoprawnyWynik()
        {
            string result = _class1.PoleKola(2.0);
            Assert.AreEqual((Math.PI * 4).ToString(), result);
        }

        [TestMethod]
        public void PoleKola_UjemnyPromien_ZwracaKomunikatOBledzie()
        {
            string result = _class1.PoleKola(-1.0);
            Assert.AreEqual("promien musi byc wiekszy od 0", result);
        }

        [TestMethod]
        public void SumaCyfr_PrawidloweDane_ZwracaPoprawnyWynik()
        {
            string result = _class1.SumaCyfr(123);
            Assert.AreEqual("liczba podzielna przez 3", result);
        }

        [TestMethod]
        public void SumaCyfr_NieprawidlowaLiczba_ZwracaKomunikatOBledzie()
        {
            string result = _class1.SumaCyfr(12);
            Assert.AreEqual("podana liczba nie jest trzycyfrowa", result);
        }

        [TestMethod]
        public void ZamienElementy_PrawidloweDane_ZwracaPoprawnaTablice()
        {
            string result = _class1.ZamienElementy(new int[] { 1, 2, 3 }, 0, 2, 1);
            Assert.AreEqual("3, 2, 1", result);
        }

        [TestMethod]
        public void ZamienElementy_NieprawidlowyIndeks_ZwracaKomunikatOBledzie()
        {
            string result = _class1.ZamienElementy(new int[] { 1, 2, 3 }, -1, 2, 1);
            Assert.AreEqual("indeks spoza zakresu", result);
        }

        [TestMethod]
        public void ZamienElementy_WarunekZero_NieZamieniaElementow()
        {
            string result = _class1.ZamienElementy(new int[] { 1, 2, 3 }, 0, 2, 0);
            Assert.AreEqual("1, 2, 3", result);
        }
    }
}



