$(function () {

    $("#DepartmentTeamFilter :input").on('input', function () {
        dataTable.ajax.reload();
    });

    //After abp v7.2 use dynamicForm 'column-size' instead of the following settings
    //$('#DepartmentTeamCollapse div').addClass('col-sm-3').parent().addClass('row');

    var getFilter = function () {
        var input = {};
        $("#DepartmentTeamFilter")
            .serializeArray()
            .forEach(function (data) {
                if (data.value != '') {
                    input[abp.utils.toCamelCase(data.name.replace(/DepartmentTeamFilter./g, ''))] = data.value;
                }
            })
        return input;
    };

    var l = abp.localization.getResource('Work');

    var service = cSP_NET.work.departmentTeams.departmentTeam;
    var createModal = new abp.ModalManager(abp.appPath + 'DepartmentTeams/DepartmentTeam/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'DepartmentTeams/DepartmentTeam/EditModal');

    var dataTable = $('#DepartmentTeamTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                visible: abp.auth.isGranted('Work.DepartmentTeam.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('Work.DepartmentTeam.Delete'),
                                confirmMessage: function (data) {
                                    return l('DepartmentTeamDeletionConfirmationMessage', data.record.id);
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
                title: l('DepartmentTeamCode'),
                data: "code"
            },
            {
                title: l('DepartmentTeamName'),
                data: "name"
            },
            {
                title: l('DepartmentTeamDescription'),
                data: "description"
            },
            {
                title: l('DepartmentTeamDepartmentID'),
                data: "departmentID"
            },
            {
                title: l('DepartmentTeamDepartment'),
                data: "department"
            },
            {
                title: l('DepartmentTeamStatus'),
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

    $('#NewDepartmentTeamButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
