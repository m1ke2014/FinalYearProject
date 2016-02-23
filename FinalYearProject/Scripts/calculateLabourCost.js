function calculateLabourCost() {
    var labourCostPerHour = document.getElementById('#LabourCostPerHour').value;
    var hours = document.getElementById('#TimeTaken').value;
    var total = parseInt(labourCostPerHour) * parseInt(hours);

    document.getElementById('#LabourCost').value = 99;//total;
}