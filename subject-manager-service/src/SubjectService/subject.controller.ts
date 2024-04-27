import {Body, Controller, Get, HttpException, HttpStatus, Post} from "@nestjs/common";
import {SubjectService} from "./subject.service";
import {CreateSubjectDto} from "./dto/CreateSubject.dto";

@Controller('subjects')
export class SubjectController {
    constructor(
        private readonly subjectService: SubjectService) {}

    @Post("create")
    async create(@Body() data: CreateSubjectDto) {
        const response = await this.subjectService.create(data.name, data.year, data.description,
            data.packet);

        if (response == undefined) {
            throw new HttpException("Unexpected error occurred", HttpStatus.INTERNAL_SERVER_ERROR);
        } else if (typeof response == "string") {
            throw new HttpException(response, HttpStatus.BAD_REQUEST);
        }

        return response;
    }

    @Get("")
    async getAll() {
        const response = await this.subjectService.getAll();

        if (response == undefined) {
            throw new HttpException("Unexpected error occurred", HttpStatus.INTERNAL_SERVER_ERROR);
        } else if (typeof response == "string") {
            throw new HttpException(response, HttpStatus.BAD_REQUEST);
        }

        return {
            subjects: response
        };
    }

}