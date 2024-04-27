import {AppConfig} from "./AppConfig";
import {DatabaseConfig} from "../Database/Config/DatabaseConfig";

export type AllConfigType = {
    app: AppConfig;
    database: DatabaseConfig;
};