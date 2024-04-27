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
        return await this.userService.create(data.username, data.password);
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