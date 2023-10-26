//$(function () {
//  $(".datepicker").datepicker();
//});

//$(function () {
//    $(".datepicker").datepicker({ minDate: 1, dateFormat: 'dd-mm-yy', });
//});


    $(document).ready(function () {
        $(".datepicker").datepicker({
            dateFormat: "dd-mm-yy",
            changemonth: true,
            changeyear: true
        });
   });
