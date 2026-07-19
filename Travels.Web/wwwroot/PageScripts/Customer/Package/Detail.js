var PackageDetail = function () {
    var me = this;
    this.Id = 0;

    getPackageItinerary = function () {
        $.ajax({
            url: '/Customer/Package/Itinerary',
            type: 'GET',
            data: { Id: me.Id },
            success: function (response) {
                $('#itineraryListDiv').html(response);
                accordionInitializer();
            }
        });
    }

    getPackageInclusion = function () {
        $.ajax({
            url: '/Customer/Package/Inclusion',
            type: 'GET',
            data: { Id: me.Id },
            success: function (response) {
                $('#inclusionListDiv').html(response);
            }
        });
    }

    getPackageHotel = function () {
        $.ajax({
            url: '/Customer/Package/Hotel',
            type: 'GET',
            data: { Id: me.Id },
            success: function (response) {
                $('#hotelListDiv').html(response);
            }
        });
    }

    getPackagePrice = function () {
        $.ajax({
            url: '/Customer/Package/Price',
            type: 'GET',
            data: { Id: me.Id },
            success: function (response) {
                $('#priceListDiv').html(response);
            }
        });
    }

    this.init = function () {
        getPackageInclusion();
        getPackageItinerary();
        getPackageHotel();
        getPackagePrice();
    }
}