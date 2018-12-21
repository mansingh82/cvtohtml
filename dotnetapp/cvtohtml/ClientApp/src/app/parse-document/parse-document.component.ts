import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-parse-document',
  templateUrl: './parse-document.component.html',
  styleUrls: ['./parse-document.component.css']
})
export class ParseDocumentComponent implements OnInit {
  public parsed: boolean;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<boolean>(baseUrl + 'api/Parser/Parse').subscribe(result => {
      this.parsed = result;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

}
