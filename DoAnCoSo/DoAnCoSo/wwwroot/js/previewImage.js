function previewImage(event) {
    var reader = new FileReader(); // Tạo một đối tượng FileReader
    reader.onload = function () {
        var imagePreviewContainer = document.getElementById('imagePreviewContainer');
        imagePreviewContainer.innerHTML = ''; // Xóa hình ảnh cũ
        var img = document.createElement('img'); // Tạo một thẻ <img>
        img.src = reader.result; // Đặt đường dẫn hình ảnh đã chọn vào thuộc tính src của thẻ <img>
        img.alt = 'Preview Image';
        imagePreviewContainer.appendChild(img); // Thêm thẻ <img> vào div chứa hình ảnh
    }
    reader.readAsDataURL(event.target.files[0]); // Đọc dữ liệu của file được chọn
}