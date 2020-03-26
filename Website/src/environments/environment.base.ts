// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
    production: false,
    adSettings: {
        tenant: '07eb46a6-f341-4af3-b282-c56f0c8c79ed',
        clientId: '81e87e5e-6fff-4e44-b670-ef4a984e2db0',
        endpoints: {
        'https://localhost/Api/': 'xxx-bae6-4760-b434-xxx',
        }
    },
    roles: {
        admin: 'f4aaa2f8-7194-409c-bcb2-d42fe7b68d08',
        hostCommittee: 'd6e0f1e8-a7f7-4a92-86a9-161c468305e0',
    }
};

/*
* For easier debugging in development mode, you can import the following file
* to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
*
* This import should be commented out in production mode because it will have a negative impact
* on performance if an error is thrown.
*/
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
