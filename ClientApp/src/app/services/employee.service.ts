import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Employee } from '../models/employee';

@Injectable({ providedIn: 'root' })
export class EmployeeService {
    private readonly apiUri = environment.apiUrl + '/api/employees/';

    constructor(private http: HttpClient) { }

    getEmployees(filter) {
        return this.http.get(this.apiUri + '?' + this.toQueryString(filter)).pipe(
            map(res => res)
        );
    }

    getEmployee(id) {
        return this.http.get(this.apiUri + id).pipe(
            map(res => res)
        );
    }

    create(employee) {
        return this.http.post(this.apiUri, employee).pipe(
            map(res => res)
        );
    }

    update(employee: Employee) {
        return this.http.put(this.apiUri + employee.id, employee).pipe(
            map(res => res)
        );
    }

    delete(id) {
        return this.http.delete(this.apiUri + id).pipe(
            map(res => res)
        );
    }

    toQueryString(obj) {
        let parts = [];
        for (let property in obj) {
            let value = obj[property];
            if (value != null && value != undefined)
                parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
        }
        return parts.join('&');
    }
}