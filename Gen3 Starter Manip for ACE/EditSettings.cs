using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Gen3_Starter_Manip_for_ACE.Types;

namespace Gen3_Starter_Manip_for_ACE
{
    public partial class EditSettings : Form
    {
        public MainForm _parent;
        public EditSettings(MainForm parent)
        {
            InitializeComponent();
            _parent = parent;
            if (!ConfigData.Instance.isSearchForACE)
            {
                FRButton.Enabled = false;
                LGButton.Enabled = false;
                SeedBulbasaur.Enabled = false;
                SeedIvysaur.Enabled = false;
                SeedVenusaur.Enabled = false;
                SeedCharmander.Enabled = false;
                SeedCharmeleon.Enabled = false;
                SeedCharizard.Enabled = false;
                SeedSquirtle.Enabled = false;
                SeedWartortle.Enabled = false;
                SeedBlastoise.Enabled = false;
            }
            if (ConfigData.Instance.version == RomVersionType.FireRed)
                FRButton.Checked = true;
            else
                LGButton.Checked = true;
            switch (ConfigData.Instance.starter)
            {
                case StarterPokemonType.Bulbasaur:
                    Bulbasaur.Checked = true;
                    break;
                case StarterPokemonType.Charmander:
                    Charmander.Checked = true;
                    break;
                case StarterPokemonType.Squirtle:
                    Squirtle.Checked = true;
                    break;
            }
            if (ConfigData.Instance.isSearchForACE)
                AceModeButton.Checked = true;
            else
                NoAceModeButton.Checked = true;
            switch (ConfigData.Instance.seedPokemon)
            {
                case SeedPokemonType.Bulbasaur:
                    SeedBulbasaur.Checked = true;
                    break;
                case SeedPokemonType.Ivysaur:
                    SeedIvysaur.Checked = true;
                    break;
                case SeedPokemonType.Venusaur:
                    SeedVenusaur.Checked = true;
                    break;
                case SeedPokemonType.Charmander:
                    SeedCharmander.Checked = true;
                    break;
                case SeedPokemonType.Charmeleon:
                    SeedCharmeleon.Checked = true;
                    break;
                case SeedPokemonType.Charizard:
                    SeedCharizard.Checked = true;
                    break;
                case SeedPokemonType.Squirtle:
                    SeedSquirtle.Checked = true;
                    break;
                case SeedPokemonType.Wartortle:
                    SeedWartortle.Checked = true;
                    break;
                case SeedPokemonType.Blastoise:
                    SeedBlastoise.Checked = true;
                    break;
            }
        }

        private void FRButton_CheckedChanged(object sender, EventArgs e)
        {
            if (FRButton.Checked)
            {
                ConfigData.Instance.version = RomVersionType.FireRed;
                string filePath = "corrupted_pokemon_FR.csv";
                Resources.loadCorruptedPokemonData(filePath);
                if (_parent is MainForm mainForm)
                    mainForm.CalcStartCall();
            }
        }
        private void LGButton_CheckedChanged(object sender, EventArgs e)
        {
            if (LGButton.Checked)
            {
                ConfigData.Instance.version = RomVersionType.LeafGreen;
                string filePath = "corrupted_pokemon_LG.csv";
                Resources.loadCorruptedPokemonData(filePath);
                if (_parent is MainForm mainForm)
                    mainForm.CalcStartCall();
            }
        }

        private void Spieces_ChekedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                if (rb.Checked)
                {
                    switch (rb.Name)
                    {
                        case "Bulbasaur":
                            ConfigData.Instance.starter = StarterPokemonType.Bulbasaur;
                            break;
                        case "Charmander":
                            ConfigData.Instance.starter = StarterPokemonType.Charmander;
                            break;
                        case "Squirtle":
                            ConfigData.Instance.starter = StarterPokemonType.Squirtle;
                            break;
                    }
                }
            }
            if (_parent is MainForm mainForm)
                mainForm.SpiecesChanged();
        }

        private void AceModeButton_CheckedChanged(object sender, EventArgs e)
        {
            if (AceModeButton.Checked)
            {
                ConfigData.Instance.isSearchForACE = true;
                FRButton.Enabled = true;
                LGButton.Enabled = true;
                SeedBulbasaur.Enabled = true;
                SeedIvysaur.Enabled = true;
                SeedVenusaur.Enabled = true;
                SeedCharmander.Enabled = true;
                SeedCharmeleon.Enabled = true;
                SeedCharizard.Enabled = true;
                SeedSquirtle.Enabled = true;
                SeedWartortle.Enabled = true;
                SeedBlastoise.Enabled = true;
            }
            if (_parent is MainForm mainForm)
            {
                mainForm.AceModeView();
                mainForm.CalcStartCall();
            }
        }

        private void NoAceModeButton_CheckedChanged(object sender, EventArgs e)
        {
            if (NoAceModeButton.Checked)
            {
                ConfigData.Instance.isSearchForACE = false;
                FRButton.Enabled = false;
                LGButton.Enabled = false;
                SeedBulbasaur.Enabled = false;
                SeedIvysaur.Enabled = false;
                SeedVenusaur.Enabled = false;
                SeedCharmander.Enabled = false;
                SeedCharmeleon.Enabled = false;
                SeedCharizard.Enabled = false;
                SeedSquirtle.Enabled = false;
                SeedWartortle.Enabled = false;
                SeedBlastoise.Enabled = false;
            }
            if (_parent is MainForm mainForm)
            {
                mainForm.NoAceModeView();
                mainForm.CalcStartCall();
            }
        }

        private void SeedPokemons_CheckedChanged_1(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                if (rb.Checked)
                {
                    switch (rb.Name)
                    {
                        case "SeedBulbasaur":
                            ConfigData.Instance.seedPokemon = SeedPokemonType.Bulbasaur;
                            break;
                        case "SeedIvysaur":
                            ConfigData.Instance.seedPokemon = SeedPokemonType.Ivysaur;
                            break;
                        case "SeedVenusaur":
                            ConfigData.Instance.seedPokemon = SeedPokemonType.Venusaur;
                            break;
                        case "SeedCharmander":
                            ConfigData.Instance.seedPokemon = SeedPokemonType.Charmander;
                            break;
                        case "SeedCharmeleon":
                            ConfigData.Instance.seedPokemon = SeedPokemonType.Charmeleon;
                            break;
                        case "SeedCharizard":
                            ConfigData.Instance.seedPokemon = SeedPokemonType.Charizard;
                            break;
                        case "SeedSquirtle":
                            ConfigData.Instance.seedPokemon = SeedPokemonType.Squirtle;
                            break;
                        case "SeedWartortle":
                            ConfigData.Instance.seedPokemon = SeedPokemonType.Wartortle;
                            break;
                        case "SeedBlastoise":
                            ConfigData.Instance.seedPokemon = SeedPokemonType.Blastoise;
                            break;
                    }
                }
            }
            if (_parent is MainForm mainForm)
                mainForm.CalcStartCall();
        }
    }
}
