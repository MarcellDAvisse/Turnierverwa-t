using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace WebbAppDB_Test
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string sqlstring = this.txtSelect.Text;
            
            string connStr = "server=localhost;database=webbapp;port=3306;user id=WebApp;password=root";

            MySqlConnection conn = new MySqlConnection(connStr);
            try { 
            
                conn.Open();
            }
            catch (MySqlException)
            {
                //Datenbank nicht verfügbar
                TableRow newRow = new TableRow();
                TableCell newCell = new TableCell();
                newCell.Text = "Datenbank nicht erreichbar";
                newRow.Cells.Add(newCell);
                tblSelect.Rows.Add(newRow);
                return;
            }


            MySqlCommand command = new MySqlCommand(sqlstring, conn);

            //Abfrage ausführen
            MySqlDataReader rdr = command.ExecuteReader();

            //Tabelle einrichten
            TableHeaderRow Header = new TableHeaderRow();
            TableHeaderCell Cell = new TableHeaderCell();
            TableRow NewRow = new TableRow();
            TableCell NewCell = new TableCell();
            //Header der Tabelle
            for (int spaltenindex = 0; spaltenindex < rdr.FieldCount; spaltenindex++)
            {

                Cell = new TableHeaderCell();
                Cell.Text = rdr.GetName(spaltenindex);
                Header.Cells.Add(Cell);
            }
            this.tblSelect.Rows.Add(Header);

            //Datensätze in die Tabelle
            while (rdr.Read())
            {
                NewRow = new TableRow();
                for (int spaltenindex = 0; spaltenindex < rdr.FieldCount; spaltenindex++)
                {
                    NewCell = new TableCell();
                    NewCell.Text = rdr.GetValue(spaltenindex).ToString();
                    NewRow.Cells.Add(NewCell);
                }
                this.tblSelect.Rows.Add(NewRow);
            }

            //Reader schliessen
            rdr.Close();
            //Verbindung schliessen
            conn.Close();
        }
    }
}