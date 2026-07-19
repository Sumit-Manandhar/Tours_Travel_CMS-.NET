var CreateAgents = function () {
    var me = this;
    var addSubs = false;
    this.ContactBranchId = 0;
    this.init = function () {

        $("#UpdateAgentForm").on("submit", function (e) {
            e.preventDefault();
            if ($("#UpdateAgentForm").valid()) {
                var formData = new FormData();
                var form = $("#UpdateAgentForm");
                var serializeForm = form.serializeArray()
                // Append form data
                $.each(serializeForm, function (index, field) {
                    formData.append(field.name, field.value);
                });

                
                $.ajax({
                    url: '/Admin/ContactBranches/SaveAgents',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (response) {
                        
                        if (response.succeeded)
                            showSuccessMessage(response.message, function () {

                                window.location.href = "/admin/ContactBranches/Agents/" + me.ContactBranchId
                            })
                        else
                            showErrorMessage(response.message)

                    },
                    error: function (xhr, status, error) {
                        showErrorMessage(error)

                    }
                });
            }
        });
    }
}