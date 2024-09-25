function myFunction() {
    document.getElementById("myDropdown").classList.toggle("show");
}
function CreateNews() {
    document.getElementById("CreateNews").classList.toggle("show");
}
function EditNews() {
    document.getElementById("editnew").classList.toggle("show");
}
var loadFile = function (event) {
    var output = document.getElementById('ballimg');
    output.src = URL.createObjectURL(event.target.files[0]);
    output.onload = function () {
        URL.revokeObjectURL(output.src) // free memory
    }
};

window.onclick = function (even) {
    if (!even.target.matches('.dropbtn')) {
        console.log("clik")
        var dropdowns = document.getElementsByClassName("dropdown-content");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}
$(document).ready(function () {
    $('.Editbtn').click(function () {
        var itemId = $(this).data('id');

        $.ajax({
            url: '/MyCart/EditCart', // 替換為實際的詳細資料Action URL
            method: 'GET',
            data: { CartId: itemId },
            success: function (response) {
                $('#details').html(response);
            },
            error: function () {
                alert('無法獲取商品詳細資料。');
            }
        });
    });
    $('.addCart').click(function () {
        var ballid = $(this).data('bid');

        $.ajax({
            url: '/Bowling/CreateCart',
            method: 'POST',
            data: { bid: ballid },
            success: function () {
                alert('成功添加到購物車');
            },
            error: function () {
                alert('添加失敗');
            }
        });
    });
});
