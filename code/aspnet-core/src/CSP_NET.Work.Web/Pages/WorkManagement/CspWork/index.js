$(function () {

    $("#CspWorkFilter :input").on('input', function () {
        dataTable.ajax.reload();
    });

    //After abp v7.2 use dynamicForm 'column-size' instead of the following settings
    //$('#CspWorkCollapse div').addClass('col-sm-3').parent().addClass('row');

    var getFilter = function () {
        var input = {};
        $("#CspWorkFilter")
            .serializeArray()
            .forEach(function (data) {
                if (data.value != '') {
                    input[abp.utils.toCamelCase(data.name.replace(/CspWorkFilter./g, ''))] = data.value;
                }
            })
        return input;
    };

    var l = abp.localization.getResource('Work');

    var service = cSP_NET.work.workManagement.cspWork;
    var createModal = new abp.ModalManager(abp.appPath + 'WorkManagement/CspWork/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'WorkManagement/CspWork/EditModal');

    var dataTable = $('#CspWorkTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                visible: abp.auth.isGranted('Work.CspWork.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('Work.CspWork.Delete'),
                                confirmMessage: function (data) {
                                    return l('CspWorkDeletionConfirmationMessage', data.record.id);
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
                title: l('CspWorkWorkCode'),
                data: "workCode"
            },
            {
                title: l('CspWorkTaskCode'),
                data: "taskCode"
            },
            {
                title: l('CspWorkStartDate'),
                data: "startDate"
            },
            {
                title: l('CspWorkEstimate'),
                data: "estimate"
            },
            {
                title: l('CspWorkDueDate'),
                data: "dueDate"
            },
            {
                title: l('CspWorkProgress'),
                data: "progress"
            },
            {
                title: l('CspWorkDuration'),
                data: "duration"
            },
            {
                title: l('CspWorkPriority'),
                data: "priority"
            },
            {
                title: l('CspWorkComplexity'),
                data: "complexity"
            },
            {
                title: l('CspWorkDegreeOfImportant'),
                data: "degreeOfImportant"
            },
            {
                title: l('CspWorkDescription'),
                data: "description"
            },
            {
                title: l('CspWorkOwner'),
                data: "owner"
            },
            {
                title: l('CspWorkAssigner'),
                data: "assigner"
            },
            {
                title: l('CspWorkPerformer'),
                data: "performer"
            },
            {
                title: l('CspWorkcollaborators'),
                data: "collaborators"
            },
            {
                title: l('CspWorkMembers'),
                data: "members"
            },
            {
                title: l('CspWorkCheckLists'),
                data: "checkLists"
            },
            {
                title: l('CspWorkProjectId'),
                data: "projectId"
            },
            {
                title: l('CspWorkApproveId'),
                data: "approveId"
            },
            {
                title: l('CspWorkName'),
                data: "name"
            },
            {
                title: l('CspWorkFullName'),
                data: "fullName"
            },
            {
                title: l('CspWorkCode'),
                data: "code"
            },
            {
                title: l('CspWorkLevel'),
                data: "level"
            },
            {
                title: l('CspWorkParent'),
                data: "parent"
            },
            {
                title: l('CspWorkParentId'),
                data: "parentId"
            },
            {
                title: l('CspWorkChildren'),
                data: "children"
            },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewCspWorkButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
