import {Body, Controller, Get, HttpException, HttpStatus, Post, Query, UseGuards} from "@nestjs/common";
import {UserService} from "./user.service";
import {CreateUserDto} from "./dto/CreateUser.dto";
import {LoginDto} from "./dto/Login.dto";
import {IsAdmin} from "./guards/IsAdmin";
import {GetAllUsersDto} from "./dto/GetAllUsers.dto";

@Controller('users')
export class UserController {
    constructor(
        private readonly userService: UserService) {}

    @Post("register")
    async register(@Body() data: CreateUserDto) {
        const response = await this.userService.create(data.identifier,
            data.username, data.password);

        if (response == undefined) {
            throw new HttpException("Unexpected error occurred", HttpStatus.INTERNAL_SERVER_ERROR);
        } else if (typeof response == "string") {
            throw new HttpException(response, HttpStatus.BAD_REQUEST);
        }

        return response;
    }

    @Post("login")
    async login(@Body() data: LoginDto) {
        const response = await this.userService.login(data.username, data.password);

        if (response == undefined) {
            throw new HttpException("User does not exist!", HttpStatus.NOT_FOUND);
        }

        return response;
    }

    @Get("all")
    @UseGuards(IsAdmin)
    async getAll(@Query() data: GetAllUsersDto) {
        const response = await this.userService.getAll();

        if (response == undefined) {
            throw new HttpException("Unexpected error occurred!", HttpStatus.NOT_FOUND);
        }

        return {
            users: response
        };
    }
}