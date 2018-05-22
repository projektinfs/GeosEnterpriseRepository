using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeosEnterprise
{
    public static class Config
    {
        /// <summary>
        /// True - jeśli chcemy wciąż trzymać usunięte rekordy w bazie
        /// </summary>
        public static bool DoNotDeletePermanently = true;
        /// <summary>
        /// True - jeżeli w trybie debugowania proces autentykacji ma zostać pominięty
        /// </summary>
        public static bool IgnoreAuthentication = true;

        /// <summary>
        /// True - jeżeli chcemy, by przy każdej zmianie modelu baza generowała się od nowa (UWAGA NA PRODUKCJI!)
        /// </summary>
        public static bool DropAndCreateWhenModelChanges = true;

        public static void MsgBoxValidationMessage(string errors)
        {
            MessageBox.Show($"Nie wszystkie dane zostały uzupełnione.\r\n" +
                $"Proszę poprawić następujące błędy:\r\n\r\n{errors}","Błąd walidacji danych", MessageBoxButtons.OK);
        }

        public static void MsgBoxNothingSelectedMessage()
        {
            MessageBox.Show("Nie wybrano żadnego obiektu", "Błąd!", MessageBoxButtons.OK);
        }
    }
}
