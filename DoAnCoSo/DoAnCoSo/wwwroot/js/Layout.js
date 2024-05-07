


function toggleMenu() {
    var menuItems = document.querySelectorAll('.container-menu-item-content');
    var content = document.querySelector('.container-content');
    menuItems.forEach(function (item) {
        if (item.style.display === 'none') {
            item.style.display = 'block';
            content.style.paddingLeft = '135px';
        } else {
            item.style.display = 'none';
            content.style.paddingLeft = '36px';
        }
    });
}