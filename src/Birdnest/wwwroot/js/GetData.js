let timerId = setInterval(() => getItems(), 2000);
const dataTable = document.getElementById("dataTable")      


let i = 10

const getItems = () =>
{
    fetch('/api/ViolationsData')
        .then(response => response.json())
        .then(data => createTable(data))
        .catch(error => console.error('Unable to get items.', error));
}

const createTable = (data) =>
{
    try
    {
        console.log(data)
        //table columns in order - PilotID, FirstName, LastName, Email, PhoneNumber,
        //Time, Distance, Duration, ViolationLocationX, ViolationLocationY
        let rowsHtml = "<thead> <tr> " + "<th> PilotID </th> " + "<th> First Name </th> "
                            + "<th> Last Name </th> " + "<th> Email </th> "
                            + "<th> Phone Number </th> " + "<th> Last Detected At </th> "
                            + "<th> Closest Distance </th> " + "<th> Overall Duration (sec) </th> "
                            + "<th> X coordinates </th> " + "<th> Y coordinates </th> " + "</tr> </thead>"

        rowsHtml += "<tbody>"
        data.forEach(function (entry, i)
        {

            rowsHtml += `<tr> <td> ${entry.pilotID} </td> <td> ${entry.firstName} </td> <td> ${entry.lastName} </td>`
                + `<td> ${entry.email} </td> <td> ${entry.phoneNumber} </td> <td> ${entry.time} </td>`
                + `<td> ${entry.distance / 1000}m </td> <td> ${entry.duration}s </td> `
                + `<td> ${entry.violationLocationX} </td> <td> ${entry.violationLocationY} </td> </tr>`
        })
        rowsHtml += "</tbody>"
        dataTable.innerHTML = rowsHtml
        /*
        const canvas = document.getElementById("coordinatePlot")
        const context = canvas.getContext('2d')
        context.beginPath()
        context.arc(150, 75, i, 0, Math.PI * 2)
        context.stroke()
        i += i
        */
    }
    catch (error)
    {
        console.log(error)
    }
}

