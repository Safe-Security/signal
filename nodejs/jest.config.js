module.exports = {
    roots: ["<rootDir>/tests"],
    moduleFileExtensions: ["js", "ts"],
    transform: {
        "\\.(ts)$": "ts-jest"
    },
    testRegex: "(/__tests__/.*|(\\.|/)(test|spec))\\.(ts)$"
};
