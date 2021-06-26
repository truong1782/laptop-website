var loadFile = function (event) {
	var image = document.getElementById('output');
	image.src = URL.createObjectURL(event.target.files[0]);

};

function confirmAction() {
	let dialog = confirm("Bạn có muốn xóa sản phẩm này ra khỏi danh sách ?");
	if (dialog == true) {
		alert("Đã xóa sản phẩm ra khỏi danh sách");
	} else {
		return false;
	}
}