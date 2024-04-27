import {Body, Controller, Get, HttpException, HttpStatus, Post, Put, Query, UseGuards} from "@nestjs/common";
import {IdentifierService} from "./identifier.service";
import {CreateIdentifierDto} from "./dto/CreateIdentifierDto";
import {UseIdentifierDto} from "./dto/UseIdentifier.dto";
import {IsAdmin} from "../UserService/guards/IsAdmin";
import {GetAllIdentifiersDto} from "./dto/GetAllIdentifiers.dto";

@Controller('identifier')
export class IdentifierController {
    constructor(
        private readonly identifierService: IdentifierService) {}

    @Post("create")
    @UseGuards(IsAdmin)
    async create(@Body() data: CreateIdentifierDto) {
        const response = await this.identifierService.create(data.role, data.name,
            data.credits, data.grade, data.year);

        if (response == undefined)
            throw new HttpException("Unexpected error", HttpStatus.INTERNAL_SERVER_ERROR)
        else if (typeof response == "string")
            throw new HttpException(response, HttpStatus.BAD_REQUEST);

        return response;
    }

    // @Post("use")
    // @UseGuards(IsAdmin)
    // async use(@Body() data: UseIdentifierDto) {
    //     const response  = this.identifierService.use(data.value);
    //
    //     return response;
    // }

    @Get("all")
    @UseGuards(IsAdmin)
    async getAll(@Query() data: GetAllIdentifiersDto) {
        const response = await this.identifierService.getAll();

        return {
            identifiers: response
        };
    }
}