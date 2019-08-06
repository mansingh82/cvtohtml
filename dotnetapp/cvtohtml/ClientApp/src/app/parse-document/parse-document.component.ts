import { Component, OnInit, Inject, ViewChild, ElementRef, PACKAGE_ROOT_URL } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-parse-document',
  templateUrl: './parse-document.component.html',
  styleUrls: ['./parse-document.component.css']
})
export class ParseDocumentComponent implements OnInit {
  public parsed: string;
  public fileLocation: string;
  public statusMessage: string = '';
  public taskStatus: boolean = true;
  
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  ngOnInit() {
  }

  onInputChange() {
    this.taskStatus = true;
    this.statusMessage = '';
  }

  saveAsHTML() {
    this.http.get(this.baseUrl + 'api/Parser/Parse?fileLocation=' + this.fileLocation).subscribe((result: boolean) => {
      this.taskStatus = result;
      if (this.taskStatus) {
        this.statusMessage = "File successfully converted to HTML, and stored in output folder of bin/(release/debug)";
      } else {
        this.statusMessage = "File parse error, file should be either of type .doc or .docx";
      }
    }, error => console.error(error));
  }
}
