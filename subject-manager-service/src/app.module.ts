import {TypeOrmModule} from "@nestjs/typeorm";
import {Module} from "@nestjs/common";
import {ConfigModule, ConfigService} from "@nestjs/config";
import databaseConfig from "./Database/Config/DatabaseConfig";
import {TypeOrmConfigService} from "./Database/TypeormConfig";
import {DataSource, DataSourceOptions} from "typeorm";
import { join } from "path";
import {UserModule} from "./UserService/user.module";
import {IdentifierModule} from "./IdentifierService/identifier.module";

const infrastructureDatabaseModule = TypeOrmModule.forRootAsync({
        useClass: TypeOrmConfigService,
        dataSourceFactory: async (options: DataSourceOptions) => {
            return new DataSource(options).initialize();
        },
    });

@Module({
    imports: [
        ConfigModule.forRoot({
            load: [databaseConfig],
            envFilePath: [".env"],
            isGlobal: true
        }),
        TypeOrmModule.forRootAsync({
            imports: [ConfigModule],
            useFactory: (configService: ConfigService) => ({
                type: configService.get('database.type', { infer: true }),
                host: configService.get('database.host', { infer: true }),
                port: configService.get('database.port', { infer: true }),
                username: configService.get('database.username', { infer: true }),
                password: configService.get('database.password', { infer: true }),
                database: configService.get('database.name', { infer: true }),
                synchronize: configService.get('database.synchronize', {
                    infer: true,
                }),
                dropSchema: false,
                keepConnectionAlive: true,
                logging:
                    configService.get('app.nodeEnv', { infer: true }) !== 'production',
                entities: [join(__dirname, '**', '*.entity.{ts,js}')],
                cli: {
                    entitiesDir: 'src',

                    subscribersDir: 'subscriber',
                },
                extra: {
                    max: configService.get('database.maxConnections', { infer: true }),
                    ssl: configService.get('database.sslEnabled', { infer: true })
                        ? {
                            rejectUnauthorized: configService.get(
                                'database.rejectUnauthorized',
                                { infer: true },
                            ),
                            ca:
                                configService.get('database.ca', { infer: true }) ??
                                undefined,
                            key:
                                configService.get('database.key', { infer: true }) ??
                                undefined,
                            cert:
                                configService.get('database.cert', { infer: true }) ??
                                undefined,
                        }
                        : undefined,
                },
            }) as any,
            inject: [ConfigService]
        }),
        UserModule,
        IdentifierModule,

    ],
})
export class AppModule {
}