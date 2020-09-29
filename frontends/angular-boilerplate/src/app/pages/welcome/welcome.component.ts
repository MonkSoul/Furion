import { Component, OnInit } from '@angular/core';
import { PersonService } from '../../services/remote/api/person.service';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.css'],
})
export class WelcomeComponent implements OnInit {
  constructor(private personService: PersonService) {}

  // 服务器返回数据
  serverData: Array<any> = [];

  ngOnInit() {
    this.personService.apiPersonGet().subscribe((data) => {
      this.serverData = data.data;
    });
  }
}
