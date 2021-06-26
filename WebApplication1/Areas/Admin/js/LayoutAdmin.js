var loadFile = function (event) {
	var image = document.getElementById('output');
	image.src = URL.createObjectURL(event.target.files[0]);
};

function confirmAction() {
	let dialog = confirm("Bạn có muốn xóa sản phẩm này ra khỏi danh sách ?");
	if (dialog == true) {
		return true;
	} else {
		return false;
	}
}