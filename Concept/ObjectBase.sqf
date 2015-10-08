[
    [//Class Register
        "ClassOrInterface1",
        "ClassOrInterface2",
        "ClassOrInterfaceN"
    ],
    [//Class List
        [//ClassOrInterface1
            [//Function/Variable Register
                "FunctionOrVar1",
                "FunctionOrVar2",
                "FunctionOrVarN"
            ],
            [//Function/Variable List ((all functions/variables are inside of arrays so they can be refered to other classes/interfaces))
                ...
            ],
            [//META-Informations
                "ClassOrInterface1" //Class name
            ]
        ],
        [//ClassOrInterface2
            [//Function/Variable Register
                "FunctionOrVar1",
                "FunctionOrVar2",
                "FunctionOrVarN"
            ],
            [//Function/Variable List
                ...
            ],
            [//META-Informations
                "ClassOrInterface2" //Class name
            ]
        ],
        [//ClassOrInterfaceN
            [//Function/Variable Register
                "FunctionOrVar1",
                "FunctionOrVar2",
                "FunctionOrVarN"
            ],
            [//Function/Variable List
                ...
            ],
            [//META-Informations
                "ClassOrInterfaceN" //Class name
            ]
        ]
    ],
    <CurrentActiveClass(RefrencedFromClassList,ChangesWhenPassingToOtherFunctionInCastedWay)>,
    [//PreSettled field for future META-Informations
        
    ]
]