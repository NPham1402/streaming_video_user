function ListenClick() {
    if (@ViewBag.MessagePassword!== null) {
        alert(@ViewBag.MessagePassword);
    }
            else {
        alert(@ViewBag.MessagePasswordFailed);
    }
}