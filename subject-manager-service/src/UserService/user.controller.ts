import {Body, Controller, HttpException, HttpStatus, Post} from "@nestjs/common";
import {UserService} from "./user.service";
import {CreateUserDto} from "./dto/CreateUser.dto";
import {LoginDto} from "./dto/Login.dto";

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
}