using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace GDAPPWIN
{
    public partial class CustomerListForm : Form
    {
        // Property to hold the selected customers
        public List<CustomerDataAccess> SelectedCustomers { get; private set; }

        // Constructor
        public CustomerListForm(DataTable customerData)
        {
            InitializeComponent();
            // Bind customer data to the DataGridView
            dataGridViewCustomerList.DataSource = customerData;
            // Add checkbox column if needed
            AddCheckboxCustomerColumnIfNeeded();
            ColumnsHeaderClass.SetCustomersThaiColumnHeaders(dataGridViewCustomerList);
        }
        private void AddCheckboxCustomerColumnIfNeeded()
        {
            if (dataGridViewCustomerList.Columns["CheckboxCustomer"] == null)
            {
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                checkBoxColumn.HeaderText = "Select";
                checkBoxColumn.Name = "CheckboxCustomer";
                dataGridViewCustomerList.Columns.Insert(0, checkBoxColumn);
            }
        }

        // Method to retrieve the selected customers when OK button is clicked
        private void btnOK_Click(object sender, EventArgs e)
        {
            // Initialize the list of selected customers
            SelectedCustomers = new List<CustomerDataAccess>();

            // Loop through the DataGridView rows to find selected customers
            foreach (DataGridViewRow row in dataGridViewCustomerList.Rows)
            {
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["CheckboxCustomer"] as DataGridViewCheckBoxCell;
                if (checkBoxCell != null && Convert.ToBoolean(checkBoxCell.Value))
                {
                    // Create a new CustomerDataAccess object with selected customer's data
                    CustomerDataAccess customer = new CustomerDataAccess
                    {
                        CustomerID = row.Cells["CustomerID"].Value.ToString(),
                        CustomerName = row.Cells["Cname"].Value.ToString(),
                        // Set other properties as needed
                    };
                    // Add the selected customer to the list
                    SelectedCustomers.Add(customer);
                }
            }

            // Close the form
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Method to search for customers based on the search term
        private void SearchCustomerData(string searchTerm)
        {
            try
            {
                // Perform search only if the search term is at least 2 characters long
                if (searchTerm.Length >= 2)
                {
                    // Search for customers based on the provided search term using CustomerDataAccess
                    DataTable searchResult = CustomerDataAccess.SearchCustomers(searchTerm);

                    // Bind the search result to the DataGridView
                    dataGridViewCustomerList.DataSource = searchResult;

                    // Add checkbox column if not already added
                    AddCheckboxCustomerColumnIfNeeded();

                    // Uncheck all checkboxes
                    UncheckCustomerAllCheckboxes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching customers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler for the text changed event of the search textbox
        private void txtSearchCustomer_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtSearchCustomer.Text;
            SearchCustomerData(searchTerm);
        }
        // Method to uncheck all checkboxes in the DataGridView
        private void UncheckCustomerAllCheckboxes()
        {
            foreach (DataGridViewRow row in dataGridViewCustomerList.Rows)
            {
                row.Cells["CheckboxCustomer"].Value = false;
            }
        }
        // Method to toggle the checkbox selection
        private void ToggleCheckboxSelection(int clickedRowIndex)
        { 
            // Uncheck all checkboxes except the clicked one
            foreach (DataGridViewRow row in dataGridViewCustomerList.Rows)
            {
                DataGridViewCheckBoxCell otherChkCell = (DataGridViewCheckBoxCell)row.Cells["CheckboxCustomer"];
                otherChkCell.Value = row.Index == clickedRowIndex;
            }
        }
        private void dataGridViewCustomerList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewCustomerList.Columns["CheckboxCustomer"].Index)
            {
                ToggleCheckboxSelection(e.RowIndex);
            }
        }


    }
}
