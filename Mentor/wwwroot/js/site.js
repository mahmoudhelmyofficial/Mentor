showInPopup = (url) => {
    
    $.ajax({
        type: "GET",
        url: url + '/' + $("#btnpop").data('id'),
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal").modal('show');
        },
        error: function (err) {
            console.log(err);
        }
    })
}

$(document).ready(function () {
    $(".btn-delete").on('click', function () {
        var btn = $(this);

        var result = confirm('Are Sure that you want to delete this job ?')

        if (result) {
            $.ajax({
                url: '/Jobs/Delete/' + btn.data('id'),
                type: "POST",
                success: function () {
                    btn.parents('.parent').fadeOut();
                    toastr.success('your job deleted successfully')
                },
                error: function (err) {
                    toastr.error('something went wrong')
                    console.log(err);
                }
            })
        }
    })
})


$(document).ready(function () {
    $(".roleDelete").on('click', function () {
        var btn = $(this);

        var result = confirm('are you sure that you want to delete this student from site ??');

        if (result) {

            $.ajax({

                url: '/Roles/Delete/' + btn.data('id'),

                type: "POST",

                success: function () {
                    btn.parents(".line-parent").fadeOut();
                    toastr.success('your job deleted successfully')
                },

                error: function (err) {
                    console.log(btn.data('id'));
                    console.log(err);
                }
            })
        }
    })
})





$(document).ready(function () {
    $("#btndlt").on('click', function () {
        var btn = $(this);

        var result = confirm('are you sure that you want to delete this Application ??');

        if (result) {

            $.ajax({

                url: '/ApplyForJobs/Delete/' + btn.data('id'),

                type: "POST",

                success: function () {
                    location.reload();
                    toastr.success('your job deleted successfully')
                    console.log(btn.data('id'));
                },

                error: function (err) {
                    console.log(btn.data('id'));
                    console.log(err);
                }
            })
        }
    })
})