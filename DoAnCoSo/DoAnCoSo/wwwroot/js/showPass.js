function showPassword(event) {
    const parentDiv = event.target.closest(".form-floating-pass");
    const passField = document.querySelector("input[type='password']");
    const showBtn = document.querySelector(".show-btn i");

    showBtn.onclick = (() => {
        if (passField.type === "password") {
            passField.type = "text";
            showBtn.classList.add("hide-btn");
        } else {
            passField.type = "password";
            showBtn.classList.remove("hide-btn");
        }
    });
}
