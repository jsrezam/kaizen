import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment as env } from '../../environments/environment';
import { Employee } from '../models/employee';

@Injectable({ providedIn: 'root' })
export class EmployeeService {
    private readonly employeesEndpoint = '/api/employees/';

    constructor(private http: HttpClient) { }

    getEmployees(filter) {
        return this.http.get(this.employeesEndpoint + '?' + this.toQueryString(filter)).pipe(
            map(res => res)
        );
    }

    getEmployee(id) {
        return this.http.get(this.employeesEndpoint + id).pipe(
            map(res => res)
        );
    }

    create(employee) {
        return this.http.post(env.dev.apiUrl + this.employeesEndpoint, employee).pipe(
            map(res => res)
        );
    }

    update(employee: Employee) {
        return this.http.put(env.dev.apiUrl + this.employeesEndpoint + employee.id, employee).pipe(
            map(res => res)
        );
    }

    delete(id) {
        return this.http.delete(env.dev.apiUrl + this.employeesEndpoint + id).pipe(
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