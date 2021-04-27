// import { EmployeeService } from 'src/app/services/employee.service';

// import { Component, OnInit } from '@angular/core';
// import { ActivatedRoute, Router } from '@angular/router';
// import { Employee } from 'src/app/models/employee';
// import { ToastrService } from 'ngx-toastr';


// @Component({
//   selector: 'app-employee-form',
//   templateUrl: './employee-form.component.html',
//   styleUrls: ['./employee-form.component.css']
// })
// export class EmployeeFormComponent implements OnInit {
//   employee: Employee = {}

//   constructor(
//     private route: ActivatedRoute, //Read routes parameters
//     private router: Router,
//     private employeeService: EmployeeService,
//     private toastrService: ToastrService) {
//     route.params.subscribe(p => {
//       this.employee.id = +p['id'] || 0;
//     });
//   }

//   ngOnInit(): void {
//     if (this.employee.id)
//       this.employeeService.getEmployee(this.employee.id)
//         .subscribe((res: any) => this.employee = res)
//         , err => {
//           if (err.status == 404)
//             this.router.navigate(['/']);
//         };
//   }

//   submit() {
//     var result$ = (this.employee.id) ? this.employeeService.update(this.employee) : this.employeeService.create(this.employee);
//     result$.subscribe((employee: any) => {
//       this.toastrService.success("Data was sucessfully saved.", "Success", {
//         onActivateTick: true
//       })
//       this.router.navigate(['/employees/'])
//     });

//   }

// }
