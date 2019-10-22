using System;
using System.Collections.Generic;
using System.Text;
using TorensVanHanoi.Model;
using Xunit;

namespace TorensVanHanoi.Test
{
    public class ZonderReflectionTest
    {
        [Fact(DisplayName = "Het klassenmodel en de tests zijn compileerbaar")]
        public void AlsDezeTestSlaagtIsHetModelCompileerbaar()
        {
        }

        [Fact(DisplayName = "Klasse bestaat: Hanoi")]
        public void KlasseHanoiBestaat()
        {
            Hanoi hanoi = null;
            Assert.Null(hanoi);
        }
        [Fact(DisplayName = "Klasse bestaat: Schijf")]
        public void KlasseSchijfBestaat()
        {
            Schijf schijf = null;
            Assert.Null(schijf);
        }
        [Fact(DisplayName = "Klasse bestaat: SchijfTeGrootException")]
        public void KlasseSchijfTeGrootExceptionBestaat()
        {
            SchijfTeGrootException ex = null;
            Assert.Null(ex);
        }
        [Fact(DisplayName = "Klasse bestaat: Toren")]
        public void KlasseTorenBestaat()
        {
            Toren toren = null;
            Assert.Null(toren);
        }
        [Fact(DisplayName = "Klasse bestaat: TorenLeegException")]
        public void KlasseTorenLeegExceptionBestaat()
        {
            TorenLeegException ex = null;
            Assert.Null(ex);
        }

        [Fact(DisplayName = "Type is subklasse van Exception: SchijfTeGrootException")]
        public void SchijfTeGrootExceptionIsEenExceptionType()
        {
            var ex = new SchijfTeGrootException();
            Assert.True(ex is Exception);
        }
        [Fact(DisplayName = "Type is subklasse van Exception: TorenLeegException")]
        public void TorenLeegExceptionIsEenExceptionType()
        {
            var ex = new TorenLeegException();
            Assert.True(ex is Exception);
        }

        [Fact(DisplayName = "Hanoi heeft een constructor met 1 int parameter")]
        public void HanoiHeeftCtorMetIntParameter()
        {
            new Hanoi(7);
        }

        [Fact(DisplayName = "Hanoi heeft property met type Toren en naam StartToren")]
        public void HanoiHeeftStartToren()
        {
            var hanoi = new Hanoi(5);
            Toren toren = hanoi.StartToren;
            Assert.NotNull(toren);
        }
        [Fact(DisplayName = "Hanoi heeft property met type Toren en naam MiddelsteToren")]
        public void HanoiHeeftMiddelsteToren()
        {
            var hanoi = new Hanoi(5);
            Toren toren = hanoi.MiddelsteToren;
            Assert.NotNull(toren);
        }
        [Fact(DisplayName = "Hanoi heeft property met type Toren en naam EindToren")]
        public void HanoiHeeftEindToren()
        {
            var hanoi = new Hanoi(5);
            Toren toren = hanoi.EindToren;
            Assert.NotNull(toren);
        }

        // minstens 1 schijf in hanoi ctor => TorenLeegException
        [Fact(DisplayName = "TorenLeegException als je probeert Hanoi met 0 schijven te maken")]
        public void TorenLeegExceptionBijAanmakenHanoiMet0Schijven()
        {
            Assert.Throws<TorenLeegException>(() => new Hanoi(0));
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
            Assert.Throws<OngeldigAantalSchijvenException>(()=>new Hanoi(aantal));
        }

        // de int parameter van de ctor van Schijf mag niet 0 zijn => ArgumentException
        // de int parameter van de ctor van schijf mag niet meer dan 100 zijn => ArgumentException
        [Theory(DisplayName = "System.ArgumentException als je probeert Hanoi met negatief aantal of meer dan 100 schijven aan te maken")]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(0)]
        [InlineData(101)]
        [InlineData(1010)]
        public void OngeldigeDiameterBijAanmakenSchijf(int diameter)
        {
            Assert.Throws<ArgumentException>(()=> new Schijf(diameter));
        }

        // de int parameter van de ctor van schijf mag elke waarde van 1 tot 100 hebben (for loopke in test)
        [Theory(DisplayName = "Schijf kan gemaakt worden met diameter van 1 t.e.m. 100")]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(11)]
        [InlineData(99)]
        public void GeldigeDiameterBijAanmakenSchijf(int diameter)
        {
            var schijf = new Schijf(diameter);
        }

        [Theory(DisplayName = "De property Diameter krijgt de waarde van de parameter in de constructor van Schijf")]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(11)]
        [InlineData(99)]
        public void SchijfDiameterWaardeViaConstructor(int diameter)
        {
            var schijf = new Schijf(diameter);
            Assert.Equal(diameter, schijf.Diameter);
        }

        [Fact(DisplayName = "Toren heeft een property BovensteSchijf met type Schijf")]
        public void TorenBovensteSchijfIsReadOnlyProperty()
        {
            var toren = new Toren();
            Schijf schijf = toren.BovensteSchijf;
        }

        [Theory(DisplayName = "Je kan een schijf met diameter van 1 tot 100 altijd op een lege toren leggen")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(10)]
        [InlineData(97)]
        [InlineData(98)]
        [InlineData(99)]
        [InlineData(100)]
        public void LegSchijfMetBepaaldeDiameterOpLegeToren(int diameter)
        {
            var toren = new Toren();
            var schijf = new Schijf(diameter);
            toren.LegSchijf(schijf);
        }


        [Fact(DisplayName = "Toren.BovensteSchijf is null in een lege toren")]
        public void BovensteSchijfIsNullInEenLegeToren()
        {
            var toren = new Toren();
            Assert.Null(toren.BovensteSchijf);
        }

        [Theory(DisplayName = "Toren: LegSchijf met schijf groter dan BovensteSchijf => SchijfTeGrootException")]
        [InlineData(3, 4, 5)]
        [InlineData(3, 4, 1)]
        [InlineData(3, 2, 3)]
        [InlineData(10, 1, 2)]
        public void LegSchijfMetSchijfGroterDanBovensteSchijf(int eersteDiameter, int tweedeDiameter, int derdeDiameter)
        {
            var toren = new Toren();
            Assert.Throws<SchijfTeGrootException>(()=> {
                var eersteSchijf = new Schijf(eersteDiameter);
                toren.LegSchijf(eersteSchijf);
                var tweedeSchijf = new Schijf(tweedeDiameter);
                toren.LegSchijf(tweedeSchijf);
                var derdeSchijf = new Schijf(derdeDiameter);
                toren.LegSchijf(derdeSchijf);
            });
        }

        [Fact(DisplayName = "Toren: LegSchijf op lege toren: BovensteSchijf wordt gelegde schijf")]
        public void LegSchijfOpLegeToren()
        {
            var toren = new Toren();
            var schijf = new Schijf(77);
            toren.LegSchijf(schijf);
            Assert.Same(schijf, toren.BovensteSchijf);
        }

        [Theory(DisplayName = "Toren:LegSchijf op niet-lege toren met schijf kleiner dan bovenste schijf => gelegde schijf wordt BovensteSChijf")]
        [InlineData(55, 22)]
        [InlineData(5, 2)]
        [InlineData(2, 1)]
        [InlineData(100, 99)]
        public void LegSchijfOpNietLegeTorenCheckBovensteSchijf(int ondersteSchijfDiameter, int bovensteSchijfDiameter)
        {
            var toren = new Toren();
            var groteSchijf =  new Schijf(ondersteSchijfDiameter);
            var kleineSchijf = new Schijf(bovensteSchijfDiameter);
            toren.LegSchijf(groteSchijf);
            toren.LegSchijf(kleineSchijf);
            Assert.Same(kleineSchijf, toren.BovensteSchijf);
        }

        [Fact(DisplayName = "Toren heeft methode NeemSchijf zonder parameters met returntype Schijf")]
        public void TorenHeeftMethodeNeemSchijfZonderParameters()
        {
            Toren toren = new Toren();
            toren.LegSchijf(new Schijf(55)); // zorg dat de toren niet leeg is.
            Schijf schijf = toren.NeemSchijf();
        }


        [Fact(DisplayName = "Toren.NeemSchijf uitvoeren op lege toren => TorenLeegException")]
        public void NeemSchijfVanLegeToren()
        {
            var toren = new Toren();
            Assert.Throws<TorenLeegException>(() => toren.NeemSchijf());
        }

        [Fact(DisplayName = "Toren.NeemSchijf uitvoeren op toren met 1 schijf: genomen schijf was BovensteSchijf")]
        public void NeemSchijfGenomenSchijfWasBovensteSchijf()
        {
            var toren = new Toren();
            var groteSchijf = new Schijf(6);
            toren.LegSchijf(groteSchijf);
            var schijf = toren.BovensteSchijf;
            var genomenSchijf = toren.NeemSchijf();
            Assert.Same(schijf, genomenSchijf);
        }

        [Fact(DisplayName = "Toren.NeemSchijf uitvoeren op toren met 1 schijf: toren is nu leeg (BovensteSchijf is null)")]
        public void TorenLeegNaNemenLaatsteSchijf ()
        {
            var toren = new Toren();
            var groteSchijf = new Schijf(6);
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
            var hanoi = new Hanoi(aantalSchijven);
            var toren = hanoi.StartToren;
            // Haal schijven van de toren tot de toren leeg zou moeten zijn.
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
            var hanoi = new Hanoi(aantalSchijven);
            var toren = hanoi.StartToren;
            Assert.Equal(1, toren.BovensteSchijf.Diameter);
        }

        [Fact(DisplayName = "Een Hanoi-spel kan opgelost worden met NeemSchijf en LegSchijf voor 1 schijf")]
        public void LosHanoiOpMet1Schijf()
        {
            var hanoi = new Hanoi(1);
            var schijf = hanoi.StartToren.NeemSchijf();
            hanoi.EindToren.LegSchijf(schijf);
            Assert.Null(hanoi.StartToren.BovensteSchijf); // de eerste toren is leeg na oplossen
            Assert.Null(hanoi.MiddelsteToren.BovensteSchijf); // de tweede toren is leeg na oplossen
            Assert.NotNull(hanoi.EindToren.BovensteSchijf); // de derde toren bevat minstens 1 schijf na oplossen
            Assert.Same(schijf, hanoi.EindToren.BovensteSchijf); // de bovenste schijf van de derde toren is de schijf waarmee werd gestart
        }

        [Fact(DisplayName = "Een Hanoi-spel kan opgelost worden met NeemSchijf en LegSchijf voor 2 schijven")]
        public void LosHanoiOpMet2Schijven()
        {
            var hanoi = new Hanoi(2);
            var schijf = hanoi.StartToren.NeemSchijf();

            // los op
            hanoi.MiddelsteToren.LegSchijf(schijf);
            schijf = hanoi.StartToren.NeemSchijf();
            hanoi.EindToren.LegSchijf(schijf);
            schijf = hanoi.MiddelsteToren.NeemSchijf();
            hanoi.EindToren.LegSchijf(schijf);

            // Controleer of de oplossing juist kan zijn
            Assert.Null(hanoi.StartToren.BovensteSchijf); // de eerste toren is leeg na oplossen
            Assert.Null(hanoi.MiddelsteToren.BovensteSchijf); // de tweede toren is leeg na oplossen
            Assert.NotNull(hanoi.EindToren.BovensteSchijf); // de derde toren bevat minstens 1 schijf na oplossen
        }

        [Fact(DisplayName = "Een Hanoi-spel kan opgelost worden met NeemSchijf en LegSchijf voor 3 schijven")]
        public void LosHanoiOpMet3Schijven()
        {
            var hanoi = new Hanoi(3);

            // Los op met 3 schijven
            void verplaatsSchijf(Toren vanToren, Toren naarToren) => naarToren.LegSchijf(vanToren.NeemSchijf());
            verplaatsSchijf(hanoi.StartToren, hanoi.EindToren);
            verplaatsSchijf(hanoi.StartToren, hanoi.MiddelsteToren);
            verplaatsSchijf(hanoi.EindToren, hanoi.MiddelsteToren);
            verplaatsSchijf(hanoi.StartToren, hanoi.EindToren);
            verplaatsSchijf(hanoi.MiddelsteToren, hanoi.StartToren);
            verplaatsSchijf(hanoi.MiddelsteToren, hanoi.EindToren);
            verplaatsSchijf(hanoi.StartToren, hanoi.EindToren);

            // Controleer of de oplossing juist kan zijn
            Assert.Null(hanoi.StartToren.BovensteSchijf); // de eerste toren is leeg na oplossen
            Assert.Null(hanoi.MiddelsteToren.BovensteSchijf); // de tweede toren is leeg na oplossen
            Assert.NotNull(hanoi.EindToren.BovensteSchijf); // de derde toren bevat minstens 1 schijf na oplossen
        }

    }
}
