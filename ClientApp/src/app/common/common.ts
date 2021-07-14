
export function parseErrorsAPI(response: any): any[] {
    let resultado: any[] = [];

    if (Array.isArray(response.error)) {
        resultado = response.error;
    } else if (!Array.isArray(response.error)) {
        if (response.error) {
            if (typeof (response.error) === "string") {
                resultado.push({ description: response.error });
            } else {
                const entradas = Object.entries(response.error);
                entradas.forEach((arr: any[]) => {
                    resultado.push({ description: arr[1] });
                })
            }
        } else {
            if (typeof (response) === "string")
                resultado.push({ description: response })
        }
    }

    return resultado;
}

export function toQueryString(obj) {
    let parts = [];
    for (let property in obj) {
        let value = obj[property];
        if (value != null && value != undefined)
            parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
    }
    return parts.join('&');
}

export function isExpiredDate(modelDate: any) {
    let date = (new Date(new Date(modelDate.year, modelDate.month - 1, modelDate.day)));

    if (date <= (new Date(new Date())))
        return true
    return false;
}

export function formatDate(date: string) {
    let resp = date.substring(0, 10).split('-');
    return {
        year: resp[0],
        month: resp[1],
        day: resp[2],
    };
}

export function generateColors(colors: number) {
    let resp: any[] = []
    console.log(getRndColor(0.5, 1));
    for (let index = 0; index < colors; index++) {
        resp.push(`rgba(${getRndColor(1, 255)},${getRndColor(1, 255)},${getRndColor(1, 255)},0.8)`);
    }
    console.log(resp);
    return resp;
}

function getRndColor(min, max) {
    return Math.floor(Math.random() * (max - min + 1) + min)
}