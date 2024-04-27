import {CanActivate, ExecutionContext, Injectable} from "@nestjs/common";
import {Reflector} from "@nestjs/core";
import {UserService} from "../user.service";
import {UserRole} from "../entities/user.entity";

@Injectable()
export class IsAdmin implements CanActivate {
    constructor(
        private readonly reflector: Reflector,
        private readonly userService: UserService,
    ) {
    }

    async canActivate(context: ExecutionContext): Promise<boolean> {
        const contextHandler = context.getHandler();
        const request = context.switchToHttp().getRequest();

        try {
            const userId = request.body["userId"]
            if (userId == undefined)
                return false;

            const role = await this.userService.getRole(userId);

            return role == UserRole.ADMIN;
        } catch (err) {
            return false;
        }

    }
}