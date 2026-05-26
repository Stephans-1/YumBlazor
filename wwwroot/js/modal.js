window.showConfirmationModal = function () {
    bootstrap.Modal.getOrCreateInstance(document.getElementById('bsConfirmationModal')).show();
}
window.hideConfirmationModal = function () {
    bootstrap.Modal.getInstance(document.getElementById('bsConfirmationModal')).hide();
}