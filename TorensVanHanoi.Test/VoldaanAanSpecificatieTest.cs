using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace TorensVanHanoi.Test
{
    public class VoldaanAanSpecificatieTest
    {

        private Assembly assembly;

        public VoldaanAanSpecificatieTest()
        {
            assembly = Assembly.LoadFrom(@"..\..\..\..\TorensVanHanoi.Model\bin\Debug\netstandard2.0\TorensVanHanoi.Model.dll");
        }

        const string ns = "TorensVanHanoi.Model";

        private string Fq(string shortName) => $"{ns}.{shortName}";
        private Type Tp(string shortName) => assembly.GetType(Fq(shortName));

        [Fact(DisplayName = "Het klassenmodel en de tests zijn compileerbaar")]
        public void AlsDezeTestSlaagtIsHetModelCompileerbaar()
        {
        }

        [Theory(DisplayName = "Klasse bestaat")]
        [InlineData("Hanoi")]
        [InlineData("Schijf")]
        [InlineData("SchijfTeGrootException")]
        [InlineData("Toren")]
        [InlineData("TorenLeegException")]
        public void StructuurKlasseHanoi(string klassenaam)
        {
            Assert.NotNull(Tp(klassenaam));
        }

        [Theory(DisplayName = "Is een exception type")]
        [InlineData("SchijfTeGrootException")]
        [InlineData("TorenLeegException")]
        public void IsEenExceptionType(string exception)
        {
            var exceptionType = assembly.GetType(Fq(exception));
            Assert.NotNull(exceptionType);
            exceptionType.IsSubclassOf(typeof(Exception));
        }

        [Fact(DisplayName = "Hanoi heeft geen default constructor")]
        public void HanoiHeeftGeenDefaultConstructor()
        {
            var ctor = Tp("Hanoi").GetConstructor(Array.Empty<Type>());
            Assert.Null(ctor);
        }

        [Fact(DisplayName = "Hanoi heeft een constructor met 1 int parameter")]
        public void HanoiHeeftCtorMetIntParameter()
        {
            var ctor = Tp("Hanoi").GetConstructor(new Type[] { typeof(int) });
            Assert.NotNull(ctor);
        }

        [Fact(DisplayName = "Hanoi heeft 1 enkele constructor")]
        public void HanoiHeeftEenEnkeleCtor()
        {
            var ctors = Tp("Hanoi").GetConstructors();
            Assert.Single(ctors);
        }

        [Fact(DisplayName = "Hanoi heeft 3 properties")]
        public void HanoiHeeft4Members()
        {
            var members = Tp("Hanoi").GetMembers();
            Assert.Equal<int>(3, members.Count(m => m.MemberType == MemberTypes.Property));
        }

        [Theory(DisplayName = "Hanoi heeft property met type Toren")]
        [InlineData("StartToren")]
        [InlineData("MiddelsteToren")]
        [InlineData("EindToren")]
        public void HanoiHeeftToren(string torenNaam)
        {
            var prop = Tp("Hanoi").GetProperty(torenNaam);
            Assert.Equal(Tp("Toren"), prop.PropertyType);
        }

        // minstens 1 schijf in hanoi ctor => TorenLeegException
        [Fact(DisplayName = "TorenLeegException als je probeert Hanoi met 0 schijven te maken")]
        public void TorenLeegExceptionBijAanmakenHanoiMet0Schijven()
        {
            try
            {
                Activator.CreateInstance(Tp("Hanoi"), 0);
            }
            catch (TargetInvocationException ex) // wrapper rond exception door reflection
            {
                Assert.Equal("TorenLeegException", ex.InnerException.GetType().Name);
            }
        }

        // maximum 100 schijvenin hanoi ctor => OngeldigAantalSchijvenException
        // negatief aantal schijven in hanoi ctor => OngeldigAantalSchijvenException
        [Theory(DisplayName = "OngeldigAantalSchijvenException als je probeert Hanoi met negatief aantal of meer dan 100 schijven aan te maken")]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(101)]
        [InlineData(1000)]
        public void OngeldigAantalSchijvenExceptionBijAanmakenHanoi(int aantal)
        {
            try
            {
                Activator.CreateInstance(Tp("Hanoi"), aantal);
            }
            catch (TargetInvocationException ex) // wrapper rond exception door reflection
            {
                Assert.Equal("OngeldigAantalSchijvenException", ex.InnerException.GetType().Name);
            }
        }

        // Schijf heeft 1 ctor
        [Fact(DisplayName = "Schijf heeft 1 enkele constructor")]
        public void SchijfHeeftEenEnkeleCtor()
        {
            var ctors = Tp("Hanoi").GetConstructors();
            Assert.Single(ctors);
        }

        // ctor van schijf heeft 1 parameter en het is een int
        [Fact(DisplayName = "Schijf heeft een constructor met 1 int parameter")]
        public void SchijfHeeftCtorMetIntParameter()
        {
            var ctor = Tp("Schijf").GetConstructor(new Type[] { typeof(int) });
            Assert.NotNull(ctor);
        }

        // de int parameter van de ctor van Schijf mag niet 0 zijn => ArgumentException
        // de int parameter van de ctor van schijf mag niet meer dan 100 zijn => ArgumentException
        [Theory(DisplayName = "System.ArgumentException als je probeert Schijf met negatieve diameter of diameter > 100 aan te maken")]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(0)]
        [InlineData(101)]
        [InlineData(1010)]
        public void OngeldigeDiameterBijAanmakenSchijf(int diameter)
        {
            try
            {
                Activator.CreateInstance(Tp("Schijf"), diameter);
            }
            catch (TargetInvocationException ex) // wrapper rond exception door reflection
            {
                Assert.IsType<ArgumentException>(ex.InnerException);
            }
        }

        // de int parameter van de ctor van schijf mag elke waarde van 1 tot 100 hebben (for loopke in test)
        [Theory(DisplayName = "Schijf kan gemaakt worden met diameter van 1 t.e.m. 100")]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(11)]
        [InlineData(99)]
        public void GeldigeDiameterBijAanmakenSchijf(int diameter)
        {
            var schijf = Activator.CreateInstance(Tp("Schijf"), diameter);
        }

        // Schijf heeft een property Diameter
        [Fact(DisplayName = "Schijf heeft een property Diameter")]
        public void SchijfHeeftPropertyDiameter()
        {
            var prop = Tp("Schijf").GetProperty("Diameter");
            Assert.NotNull(prop);
        }
        // De property Diameter is read-only, dus heeft geen setter
        [Fact(DisplayName = "Schijf.Diameter is read-only, dus heeft geen setter")]
        public void SchijfDiameterIsReadOnly()
        {
            var prop = Tp("Schijf").GetProperty("Diameter");
            Assert.Null(prop.SetMethod);
        }

        [Theory(DisplayName = "De property Diameter krijgt de waarde van de parameter in de constructor van Schijf")]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(11)]
        [InlineData(99)]
        public void SchijfDiameterWaardeViaConstructor(int diameter)
        {
            dynamic schijf = Activator.CreateInstance(Tp("Schijf"), diameter);
            Assert.Equal(diameter, schijf.Diameter);
        }

        [Fact(DisplayName = "Toren heeft een public default constructor, die de constructor met 1 parameter uitvoert met waarde 0")]
        public void TorenPublicDefaultCtor()
        {
            Assert.True(Tp("Toren").GetConstructor(Array.Empty<Type>()).IsPublic);
        }

        [Fact(DisplayName = "Toren heeft een tweede constructor met 1 parameter van type int, maar die heeft geen public zichtbaarheid")]
        public void TorenInternalConstructorMetIntParameter()
        {
            var ctor = (ConstructorInfo)Tp("Toren").FindMembers(MemberTypes.Constructor, BindingFlags.Instance | BindingFlags.NonPublic, (mi, o) => true, null).First();
            Assert.Single(ctor.GetParameters());
            Assert.Equal(typeof(int), ctor.GetParameters().First().ParameterType);
        }

        [Fact(DisplayName = "Toren heeft een read-only property BovensteSchijf")]
        public void TorenBovensteSchijfIsReadOnlyProperty()
        {
            Assert.Null(Tp("Toren").GetProperty("BovensteSchijf").SetMethod);
        }

        [Fact(DisplayName = "Toren.BovensteSchijf heeft als type Schijf")]
        public void TorenBovensteSchijfHeeftTypeSchijf()
        {
            Assert.Equal(Tp("Schijf"), Tp("Toren").GetProperty("BovensteSchijf").PropertyType);
        }

        [Fact(DisplayName = "Toren heeft een publieke methode LegSchijf met 1 parameter van type Schijf")]
        public void TorenLegSchijfIsPublicMethodMet1Parameter()
        {
            var parameters = Tp("Toren").GetMethod("LegSchijf").GetParameters();
            Assert.Single(parameters);
        }

        [Fact(DisplayName = "De parameter van methode Toren.LegSchijf heeft type Schijf")]
        public void TorenLegSchijfIsPublicMethodMetParameterVanTypeSchijf()
        {
            var parameters = Tp("Toren").GetMethod("LegSchijf").GetParameters();
            Assert.Equal(Tp("Schijf"), parameters[0].ParameterType);
        }

        [Fact(DisplayName = "Toren.BovensteSchijf is null in een lege toren")]
        public void BovensteSchijfIsNullInEenLegeToren()
        {
            dynamic toren = Activator.CreateInstance(Tp("Toren"));
            Assert.Null(toren.BovensteSchijf);
        }

        [Fact(DisplayName = "Toren: LegSchijf met schijf groter dan BovensteSchijf => SchijfTeGrootException")]
        public void LegSchijfMetSchijfGroterDanBovensteSchijf()
        {
            dynamic toren = Activator.CreateInstance(Tp("Toren"));
            dynamic kleineSchijf = Activator.CreateInstance(Tp("Schijf"), 3);
            dynamic groteSchijf = Activator.CreateInstance(Tp("Schijf"), 6);
            toren.LegSchijf(kleineSchijf);
            void LegGroteSchijf() => toren.LegSchijf(groteSchijf);
            Assert.Throws(Tp("SchijfTeGrootException"), () => LegGroteSchijf());
        }

        [Fact(DisplayName = "Toren: LegSchijf op lege toren: BovensteSchijf wordt gelegde schijf")]
        public void LegSchijfOpLegeToren()
        {
            dynamic toren = Activator.CreateInstance(Tp("Toren"));
            dynamic kleineSchijf = Activator.CreateInstance(Tp("Schijf"), 3);
            toren.LegSchijf(kleineSchijf);
            Assert.Equal(kleineSchijf, toren.BovensteSchijf);
        }

        [Fact(DisplayName = "Toren:LegSchijf op niet-lege toren met schijf kleiner dan bovenste schijf => gelegde schijf wordt BovensteSChijf")]
        public void LegSchijfOpNietLegeTorenCheckBovensteSchijf()
        {
            dynamic toren = Activator.CreateInstance(Tp("Toren"));
            dynamic groteSchijf = Activator.CreateInstance(Tp("Schijf"), 6);
            dynamic kleineSchijf = Activator.CreateInstance(Tp("Schijf"), 3);
            toren.LegSchijf(groteSchijf);
            toren.LegSchijf(kleineSchijf);
            Assert.Equal(kleineSchijf, toren.BovensteSchijf);
        }

        [Fact(DisplayName = "Toren heeft methode NeemSchijf zonder parameters met returntype Schijf")]
        public void TorenHeeftMethodeNeemSchijfZonderParameters()
        {
            Assert.Empty(Tp("Toren").GetMethod("NeemSchijf").GetParameters());
            Assert.Equal(Tp("Schijf"), Tp("Toren").GetMethod("NeemSchijf").ReturnType);
        }


        [Fact(DisplayName = "Toren.NeemSchijf uitvoeren op lege toren => TorenLeegException")]
        public void NeemSchijfVanLegeToren()
        {
            dynamic toren = Activator.CreateInstance(Tp("Toren"));
            void neem() => toren.NeemSchijf();
            Assert.Throws(Tp("TorenLeegException"), () => neem());
        }

        [Fact(DisplayName = "Toren.NeemSchijf uitvoeren op toren met 1 schijf: genomen schijf was BovensteSchijf")]
        public void NeemSchijfGenomenSchijfWasBovensteSchijf()
        {
            dynamic toren = Activator.CreateInstance(Tp("Toren"));
            dynamic groteSchijf = Activator.CreateInstance(Tp("Schijf"), 6);
            toren.LegSchijf(groteSchijf);
            var schijf = toren.BovensteSchijf;
            Assert.Equal(schijf, toren.NeemSchijf());
        }

        [Fact(DisplayName = "Toren.NeemSchijf uitvoeren op toren met 1 schijf: toren is nu leeg (BovensteSchijf is null)")]
        public void TorenLeegNaNemenLaatsteSchijf()
        {
            dynamic toren = Activator.CreateInstance(Tp("Toren"));
            dynamic groteSchijf = Activator.CreateInstance(Tp("Schijf"), 6);
            toren.LegSchijf(groteSchijf);
            toren.NeemSchijf();
            Assert.Null(toren.BovensteSchijf);
        }

        [Theory(DisplayName = "Hanoi.StartToren bevat juist aantal schijven")]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public void HanoiStartTorenBevatJuistAantalSchijven(int aantalSchijven)
        {
            dynamic hanoi = Activator.CreateInstance(Tp("Hanoi"), aantalSchijven);
            var toren = hanoi.StartToren;
            for (int i = 0; i < aantalSchijven; i++)
            {
                Assert.NotNull(toren.BovensteSchijf);
                toren.NeemSchijf();
            }
            Assert.Null(toren.BovensteSchijf);
        }

        [Theory(DisplayName = "Bovenste schijf van Hanoi.StartToren heeft diameter 1.")]
        [InlineData(1)]
        [InlineData(11)]
        [InlineData(97)]
        public void BovensteSchijfInNieuweStartTorenHeeftDiameter1(int aantalSchijven)
        {
            dynamic hanoi = Activator.CreateInstance(Tp("Hanoi"), aantalSchijven);
            var toren = hanoi.StartToren;
            Assert.Equal(1, toren.BovensteSchijf.Diameter);
        }


        [Fact(DisplayName = "Een Hanoi-spel kan opgelost worden met NeemSchijf en LegSchijf voor 1 schijf")]
        public void LosHanoiOpMet1Schijf()
        {
            dynamic hanoi = Activator.CreateInstance(Tp("Hanoi"), 1);
            var schijf = hanoi.StartToren.NeemSchijf();
            hanoi.EindToren.LegSchijf(schijf);
        }

        [Fact(DisplayName = "Een Hanoi-spel kan opgelost worden met NeemSchijf en LegSchijf voor 2 schijven")]
        public void LosHanoiOpMet2Schijven()
        {
            dynamic hanoi = Activator.CreateInstance(Tp("Hanoi"), 2);
            var schijf = hanoi.StartToren.NeemSchijf();
            hanoi.MiddelsteToren.LegSchijf(schijf);
            schijf = hanoi.StartToren.NeemSchijf();
            hanoi.EindToren.LegSchijf(schijf);
            schijf = hanoi.MiddelsteToren.NeemSchijf();
            hanoi.EindToren.LegSchijf(schijf);
        }

        [Fact(DisplayName = "Een Hanoi-spel kan opgelost worden met NeemSchijf en LegSchijf voor 3 schijven")]
        public void LosHanoiOpMet3Schijven()
        {
            dynamic hanoi = Activator.CreateInstance(Tp("Hanoi"), 3);
            void verplaats(dynamic vanToren, dynamic naarToren) => naarToren.LegSchijf(vanToren.NeemSchijf());
            verplaats(hanoi.StartToren, hanoi.EindToren);
            verplaats(hanoi.StartToren, hanoi.MiddelsteToren);
            verplaats(hanoi.EindToren, hanoi.MiddelsteToren);
            verplaats(hanoi.StartToren, hanoi.EindToren);
            verplaats(hanoi.MiddelsteToren, hanoi.StartToren);
            verplaats(hanoi.MiddelsteToren, hanoi.EindToren);
            verplaats(hanoi.StartToren, hanoi.EindToren);
        }

        [Fact(DisplayName = "Facultatief: los het probleem recursief op in Toren.VerplaatsNaar(int offset, Toren target, int targetOffset, Toren via, int viaOffset")]
        public void Facultatief() { }

    }
}
