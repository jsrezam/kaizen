
export function parseErrorsAPI(response: any): any[] {
    let resultado: any[] = [];

    if (Array.isArray(response.error)) {
        resultado = response.error;
    } else if (!Array.isArray(response.error)) {
        if (typeof (response.error) === "string") {
            resultado.push({ description: response.error });
        }
        else {
            const entradas = Object.entries(response.error);
            entradas.forEach((arr: any[]) => {
                resultado.push({ description: arr[1] });
            })
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