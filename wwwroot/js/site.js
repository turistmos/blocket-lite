function show_car() {
    let category = document.getElementById("category").value;

    switch (category) {

        case "vehicle":
            document.querySelector(".forms_div").style.display = "block";
            document.getElementById("forms_div").innerHTML = '<div class="form-group"><label asp-for="year"></label><input asp-for="year" class="form-control"></div>'
            document.getElementById("forms_div").innerHTML = '<div class="form-group"><label asp-for="miles"></label><input asp-for="miles" class="form-control"></div>'

            break;
        case "cloths":

            break;
        default:
            document.querySelector(".forms_div").style.display = "block";
            break;

    }
}