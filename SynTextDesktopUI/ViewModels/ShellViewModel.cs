using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SynTextLibrary;

namespace SynTextDesktopUI.ViewModels
{
    public class ShellViewModel : Screen
    {
        private string _sampleText = "Type/paste sample text here.";

        public string SampleText
        {
            get
            {
                return _sampleText;
            }
            set
            {
                _sampleText = value;
                NotifyOfPropertyChange(() => SampleText);
                NotifyOfPropertyChange(() => CanSubmit);
            }
        }

        private string _response;

        public string Response
        {
            get
            {
                return _response;
            }
            set
            {
                _response = value;
                NotifyOfPropertyChange(() => Response);
                //NotifyOfPropertyChange(() =>CanSubmit);
            }
        }

        private string _counter;

        public string Counter
        {
            get
            {
                return _counter;
            }
            set
            {
                _counter = value;
            }
        } 




        public bool CanSubmit
        {
            get
            {
                bool output = false;
                if (SampleText?.Length > 0)
                {
                    output = true;
                }

                return output;
            }
        }

        public async Task Submit()
        {
            SampleText sample = new SampleText();
            sample.Text = SampleText;

            ResponseString responseString = await TextProcessor.LoadReadablityAsync(sample);

            Response = responseString.Text;
        }

    }
}
