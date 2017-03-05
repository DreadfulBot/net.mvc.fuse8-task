//function ExportExcel() {
//    $.ajax({
//        url: '/Report/ExportExcel'
//    }).done(function () {
//        alert('Added');
//    });
//}

function SendEmail() {
    var to = $('#emailTo').val();
    var smtpHost = $('#smtpHost').val();
    var smtpSender = $('#smtpSender').val();
    var smtpPassword = $('#smtpPassword').val();

    debugger;
    
    if (to == undefined || to === "" ||
        smtpHost == undefined || smtpHost === "" ||
        smtpSender == undefined || smtpSender === "" ||
        smtpPassword == undefined || smtpPassword === "")
    {
        alert("Неверно заполнены поля для отправки");
        return;
    }

    $.ajax({
        url: '/Report/SendEmail',
        type: "post",
        data: {
            "to" : to,
            "from" : smtpSender,
            "pass" : smtpPassword,
            "smtpHost" : smtpHost
        }
    }).done(function (e) {
        alert(e);
    });
}

$(document).ready(function () {
    $("#startDate").datepicker({ changeMonth: true, changeYear: true });
    $("#endDate").datepicker({ changeMonth: true, changeYear: true });

    //$("#exportExcelBtn").click(function(e) {
    //    e.preventDefault();
    //    ExportExcel();
    //});

    $("#sendEmailBtn").click(function (e) {
        e.preventDefault();
        SendEmail();
    });
});