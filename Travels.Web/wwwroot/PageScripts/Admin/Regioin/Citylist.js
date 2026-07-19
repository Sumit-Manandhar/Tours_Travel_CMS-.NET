
var CityList = function () {
    var pop = null;

    var EditcityEvent = function () {
        $('a[data-type="Edit"]').on('click', function () {
            var id = $(this).data('id')
            AddUpdateCity(id);
        });
        $('a[data-type="Delete"]').on('click', function () {
            var id = $(this).data('id')
            showConfirmation("Warning", "Are you sure to Delete/Restore the City?", function () {

                DeleteCity(id);
            })
        });
    }
    var initlistning = function () {
        $("#Keyword").on("keypress", function (e) {
            if (e.key === "Enter") {
                $("#SearchForm").trigger("submit");
            }
        })
        $("#btn-search").on("click", function () {
            $("#SearchForm").trigger("submit");
        })

        $('#CountryId').on('change', function () {
            $("#SearchForm").trigger("submit");

        })
        $("#SearchForm").on("submit", function (e) {
            e.preventDefault();
            var f = $("#SearchForm");
            $.ajax({
                type: 'POST',
                url: f[0].action,
                data: {
                    countryId : $('#CountryId').val(),
                    Keyword: $("#Keyword").val(),
                    pageNum: po.getNum(),
                    pageSize: po.getSize(),
                    orderBy: po.getOrder(),
                    orderByAscending: po.getAsc()
                },
                dataType: 'html',
                success: function (content, strStatus) {
                    $('#dataList').html(content);
                    EditcityEvent()
                },
            });
        });

    }

    var AddUpdateCity = function(id)
    {
        $.ajax({
            type: 'get',
            url: '/Admin/Region/AddUpdate?id='+ id,
            dataType: 'html',
            success: function (content, strStatus) {
                $('body').append(content);
                $('#addUpdateModal').modal("show");
                handleCitySubmit()
            },
        });
    }
    var DeleteCity = function (id) {
        $.ajax({
            type: 'Get',
            url: '/Admin/Region/DeleteUndeleteCity?id=' + id,
            dataType: 'json',
            success: function (response) {
                if (response.succeeded)
                    showSuccessMessage(response.message, function () {
                        po.refresh();
                    })
                else
                    showErrorMessage(response.message)
            }
        });
    }
    handleCitySubmit = function () {
        $("#btn-addUpdatecity").on("click", function (e) {
            e.preventDefault();
            if ($("#addUpdateCityForm").valid()) {
                var form = $("#addUpdateCityForm")
                $.ajax({
                    type: 'Post',
                    url: '/Admin/Region/SaveCity',
                    data: form.serialize(),
                    dataType: 'json',
                    success: function (response) {
                        if (response.succeeded)
                            showSuccessMessage(response.message, function () {
                                po.refresh();
                            })
                        else
                            showErrorMessage(response.message)
                    }
                });
            }
        });
    }
    this.init = function () {
        initlistning();
        EditcityEvent()
        po = new paginationObject();
        po.loadPage = function (pi) {
            if ($("#SearchForm").valid()) {
                $("#SearchForm").trigger('submit');
            }
        };
        po.init("#dataListContainer", "#btn-search", "select[name='pageSize']", ".page-link", ".sorting", "", "");

        $('#btn-add').on('click', function () {
            AddUpdateCity(0);
        })
    }
}