add_dotnet_executable(
    NAME
        SimpleApp
    VERSION
        ${CSSDK_MAJOR}.${CSSDK_MINOR}.${CSSDK_PATH}
    FRAMEWORK
        net6.0
    SOURCES
        Main.cs
    REFERENCES
        DolbyIO.Comms.Sdk
    PACKAGES
        "System.CommandLine_2.0.0-beta4.22272.1"
        "Serilog_2.11.0"
        "Serilog.Sinks.Console_4.0.1"
)