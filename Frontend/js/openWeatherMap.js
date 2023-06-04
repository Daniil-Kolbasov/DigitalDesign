document.getElementById("clickButton").addEventListener('click', asyncRequest);

var request = new XMLHttpRequest();

function asyncRequest() {
    console.log(1);
    var apiKey = document.getElementById("apiKey").value;
    var lat = document.getElementById("latitude").value;
    var long = document.getElementById("longitude").value;
    // var modes = document.querySelector('input[name="modes"]:checked').value;
    var units = document.querySelector('input[name="units"]:checked').value;
    var lang = document.getElementById('languageOption').value;

    // Проверак, имеются ли значения в полях Широта и Долгота
    if (!lat || !long)
        alert("Поле ширина и долгота не были заполнены");
    else {
        // Составление url
        var url = "https://api.openweathermap.org/data/2.5/weather?";
        url += "lat=" + encodeURIComponent(lat) + "&lon=" + encodeURIComponent(long) + "&units=" + units + "&lang=" + lang + "&appid=" + apiKey;

        // Привязка метода и отправка запроса
        request.onload = handlerStateChange();
        // request.onreadystatechange = null;
        request.open("GET", url, true);
        request.send();
    }
}

function handlerStateChange() {
    if (request.readyState === 4) {
        if (request.status === 200) {
            // if (modes == "json") {
            const doc = JSON.parse(request.responseText);
            const name = doc.name;
            const temp = doc.main.temp;
            const icon = doc.weather[0].icon;
            const desc = doc.weather[0].description;
            const clouds = doc.clouds.all;
            const speedWind = doc.wind.speed;

            outputWeatherForecast(name, temp, icon, desc, clouds, speedWind);
            setInterval(asyncRequest, 60000);
            // TDOO: напистьа пареср для xml
            // }
            // else {
            //     const parser = new DOMParser();
            //     const doc = parser.parseFromString(request.responseText, "application/xml");
            //     console.log(doc);
            // }
        }
        else
            alert(`Упс. Что - то пошло не так.Статус кода: ${request.status}`);
    }
}

function outputWeatherForecast(name, temp, icon, desc, clouds, speedWind) {
    document.getElementById('cityName').innerHTML = `Погода в ${name}, сейчас`;
    document.getElementById('temp').innerHTML = "Температура: " + temp;
    document.getElementById('icon').innerHTML = `<image src="image/${icon}@2x.png" alr="iconTemp" style="width: 40px;">`;
    document.getElementById('desc').innerHTML = "Описнаие: " + desc;
    document.getElementById('clouds').innerHTML = "Обласность: " + clouds + "%";
    document.getElementById('speedWind').innerHTML = "Скорость ветра: " + speedWind + "м/с";
}
