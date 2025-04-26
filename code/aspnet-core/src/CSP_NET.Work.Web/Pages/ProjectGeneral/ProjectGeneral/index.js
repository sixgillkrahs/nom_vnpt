$(function () {

    $("#ProjectGeneralFilter :input").on('input', function () {
        dataTable.ajax.reload();
    });

    //After abp v7.2 use dynamicForm 'column-size' instead of the following settings
    //$('#ProjectGeneralCollapse div').addClass('col-sm-3').parent().addClass('row');

    var getFilter = function () {
        var input = {};
        $("#ProjectGeneralFilter")
            .serializeArray()
            .forEach(function (data) {
                if (data.value != '') {
                    input[abp.utils.toCamelCase(data.name.replace(/ProjectGeneralFilter./g, ''))] = data.value;
                }
            })
        return input;
    };

    var l = abp.localization.getResource('Work');

    var service = cSP_NET.work.projectGeneral.projectGeneral;
    var createModal = new abp.ModalManager(abp.appPath + 'ProjectGeneral/ProjectGeneral/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'ProjectGeneral/ProjectGeneral/EditModal');

    var dataTable = $('#ProjectGeneralTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,//disable default searchbox
        autoWidth: false,
        scrollCollapse: true,
        order: [[0, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList,getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('Work.ProjectGeneral.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('Work.ProjectGeneral.Delete'),
                                confirmMessage: function (data) {
                                    return l('ProjectGeneralDeletionConfirmationMessage', data.record.id);
                                },
                                action: function (data) {
                                    service.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l('SuccessfullyDeleted'));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
            {
                title: l('ProjectGeneralCode'),
                data: "code"
            },
            {
                title: l('ProjectGeneralName'),
                data: "name"
            },
            {
                title: l('ProjectGeneralDepartment'),
                data: "department"
            },
            {
                title: l('ProjectGeneralState'),
                data: "state"
            },
            {
                title: l('ProjectGeneralStatus'),
                data: "status"
            },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewProjectGeneralButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
