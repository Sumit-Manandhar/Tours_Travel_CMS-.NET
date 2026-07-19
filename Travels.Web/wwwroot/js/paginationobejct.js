var paginationObject = function () {
    var me = this;
    var parent = null;

    this.getKeyword = function () {
        return $('#keyword').val();
    }

    this.setKeyword = function (val) {
        $('#keyword').val(val);
    }

    this.getNum = function () {
        return parseInt($(parent).find('#pageNum').val());
    }

    this.setNum = function (val) {
        $(parent).find('#pageNum').val(val);
    }

    this.getSize = function () {
        return parseInt($(parent).find("select[name='pageSize']").val());
    }

    this.setSize = function (val) {
        $(parent).find("select[name='pageSize']").val(val);
    }

    this.getOrder = function () {
        return $(parent).find('#orderBy').val();
    }

    this.setOrder = function (val) {
        $(parent).find('#orderBy').val(val);
    }

    this.getAsc = function () {
        return $(parent).find('#orderByAscending').val();
    }

    this.setAsc = function (val) {
        $(parent).find('#orderByAscending').val(val);
    }

    this.getTotalPageCount = function () {
        return parseInt($(parent).find("#pageCount").val());
    }

    this.loadPage = null;

    this.search = function () {
        me.setNum(1);
        me.loadPage(me);
    }

    this.refresh = function () {
        me.loadPage(me);
    }

    this.switchPage = function (page) {
        var totalPages = me.getTotalPageCount();
        var num = me.getNum();
        var ti;
        switch (page) {
            case 'first':
                ti = 1;
                break;
            case 'prev':
                ti = num - 1;
                break;
            case 'next':
                ti = num + 1;
                break;
            case 'last':
                ti = totalPages;
                break;
            default:
                ti = page;
                break;
        }
        ti = ti <= 0 ? 1 : ti;
        ti = ti > totalPages ? totalPages : ti;
        me.setNum(ti);
        me.loadPage(me);
    }

    this.sort = function (sorting) {

        if (me.getOrder() == sorting) {
            var asc = me.getAsc() == 'True' ? 'False' : 'True';
            me.setAsc(asc);
        }
        else {
            me.setOrder(sorting);
            me.setAsc('True');
        }
        me.loadPage(me);
    }

    this.init = function (root, btnSearch, selectSize, aPage, aSort, selectAll, selectItem) {
        if (root == null || root == '')
            throw "root is not defined";

        parent = root;

        if (btnSearch != null && btnSearch != '') {
            $(root).on("click", btnSearch, function () {
                me.search();
            });
        }

        if (selectSize != null && selectSize != '') {
            $(root).on("change", selectSize, function () {
                me.search();
            });
        }

        if (aPage != null && aPage != '') {
            $(root).on('click', aPage, function () {
                var obj = $(this);
                var page = obj.data('page');
                me.switchPage(page);
            });
        }

        if (aSort != null && aSort != '') {
            $(root).on('click', aSort, function () {
                var obj = $(this);
                var sorting = obj.data('column');
                me.sort(sorting);
            });
        }

        if (selectAll != null && selectAll != '') {
            $(root).on('change', selectAll, function () {
                var checked = $(this).prop('checked');
                $(selectItem).prop('checked', checked);
            });
        }

        if (selectItem != null && selectItem != '') {
            $(root).on('change', selectItem, function () {
                var count = $(selectItem).length;
                var checkedCount = $(selectItem + ":checked").length;
                $(selectAll).prop('checked', count == checkedCount);
            });
        }
    }

}
