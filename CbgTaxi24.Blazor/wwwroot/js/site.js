
//investigate weird behaviour
//window.bootstrapInteropt = {
//    showModal: id => {
//        console.log("show modal in js");
//        var myModal = new bootstrap.Modal(document.getElementById(id));
//        myModal.show();
//    },
//    hideModal: id => {
//        console.log("close modal from js");
//        var myModal2 = new bootstrap.Modal(document.getElementById(id), { backdrop: false });
//        myModal2.hide();
//        myModal2.dispose();
//    },

//    getSelectedValues: sel => {
//        var results = [];
//        var i;
//        for (i = 0; i < sel.options.length; i++) {
//            if (sel.options[i].selected) {
//                results[results.length] = sel.options[i].value;
//            }
//        }
//        return results;
//    }
//};

window.bootstrapInteropt = {
    showModal: id => {
        console.log("show modal in js");
        $(`#${id}`).modal({ backdrop: 'static' });
    },
    hideModal: id => {
        //console.log("close modal from js");
        $(`#${id}`).modal('hide');
    },

    getSelectedValues: sel => {
        var results = [];
        var i;
        for (i = 0; i < sel.options.length; i++) {
            if (sel.options[i].selected) {
                results[results.length] = sel.options[i].value;
            }
        }
        return results;
    },

    clearSearch: id => {
        var getValue = document.getElementById(id);
        if (getValue.value != "") {
            getValue.value = "";
        }
    }
};