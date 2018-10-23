# PS4 MEMAPI Library

### Properties
| Type    | Property      | Description                                       |
| ------- | ------------- | ------------------------------------------------- |
| string  | IP            | The IP address of the PS4.                        |
| Process | ActiveProcess | The current active process to perform methods on. |

### Core Methods
| Function                        | Description                                                                                          |
| ------------------------------- | ---------------------------------------------------------------------------------------------------- |
| bool connect()                  | Connect to the PS4. Returns true/false depending on success                                          |
| void disconnect()               | Disconnect from the PS4                                                                              |
| bool attach(string processName) | Attach to the process with the process name. Returns true/false depending on success                 |
| bool attach(Process process)    | Attach to the given process. Returns true/false depending on success                                 |
| bool attach()                   | Attach to the process that is set via property AciveProcess. Returns true/false depending on success |
| bool attachEboot()              | Attach to the eboot process. Returns true/false depending on success                                 |
| void detach()                   | Detach from an attached process                                                                      |
| bool isConnected()              | Returns true if the client is connected to the PS4, false otherwise                                  |
| bool isAttached()               | Returns true if the client is attached to a process, false otherwise                                 |
| bool payload(byte[] payload)    | Send the payload bytes to the PS4                                                                    |

### Events
| Event           | Description                                      |
| --------------- | ------------------------------------------------ |
| ConnectedEvent  | Raised when a PS4 has successfully connected     |
| DisconnectEvent | Raised when a PS4 has disconnected               |
| AttachedEvent   | Raised when a process has successfully attached  |
| DetachedEvent   | Raised when a process has detached               |
| GoEvent         | Raised when a process resumes exectuion          |
| StoppedEvent    | Raised when a process stops                      |
| SteppedEvent    | Raised when a process steps a single instruction |
| KilledEvent     | Raised when a process is killed                  |
| TrappedEvent    | Raised when a hits a breakpoint trap             |

### Write/Read/Search Memory
| Function                                                      | Description                                                                                                                 |
| ------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------- |
| ErrorCode write<T>(ulong address, T[] elements)               | Generic function to write any data type array to the given address.                                                         |
| ErrorCode write<T>(ulong address, T element)                  | Generic function to write any data type to the given address.                                                               |
| T[] read<T>(ulong address, int elements, out ErrorCode error) | Generic function to read any data type array from the given address. The elements signifies the number of elements to read. |
| T read<T>(ulong address, out ErrorCode error)                 | Generic function to read any data type from the given address.                                                              |
| ErrorCode search<T>(ulong start, ulong end, T[] elements)     | Search for the array of elements in a consecutive sequence in memory between the from and to address.                       |
| ErrorCode search<T>(ulong start, ulong end, T element)        | Search for the element in memory between the from and to address.                                                           |
| ErrorCode rescan<T>(T[] elements)                             | Rescan the results from a previous search for the new array of elements in a consecutive sequence.                          |
| ErrorCode rescan<T>(T element)                                | Rescan the results from a previous search for the element.                                                                  |
| int getResultsCount()                                         | Get the number of results found in the last search or rescan.                                                               |
| ulong[] getResults()                                          | Get an array of result addresses found in the last search or scan.                                                          |
| ErrorCode endSearch()                                         | End the last search / rescan and clean up allocated resources.                                                              |

### Misc
| Function                                                                | Description                                                  |
| ----------------------------------------------------------------------- | ------------------------------------------------------------ |
| List<Process> getProcesses(out ErrorCode error)                         | Get a list of all running processes on the PS4               |
| List<Thread> getProcessThreads(out ErrorCode error, int processId = -1) | Get a list of all running threads for a given process        |
| ErrorCode notify(string message)                                        | Send a notification to the PS4                               |
| List<Module> getModules(out ErrorCode error)                            | Get a list of active modules on the PS4                      |
| List<MemoryRange> getRegions(out ErrorCode error)                       | Get a list of mapped memory regions for the current process. |
| string getFirmware()                                                    | Get the current running firmware version. (Eg: "5.05")       |

### Debugging
| Function                                                          | Description                                         |
| ----------------------------------------------------------------- | --------------------------------------------------- |
| ErrorCode go()                                                    | Resume a stopped process                            |
| ErrorCode stop()                                                  | Stop a process from running                         |
| ErrorCode step()                                                  | Execute a single instruction                        |
| ErrorCode kill()                                                  | Kill a process                                      |
| Registers getRegisters(out ErrorCode error)                       | Get the registers for the current state             |
| ErrorCode setRegisters(Registers regs)                            | Set the registers for the current state             |
| ErrorCode resetHardwareBreakpoint(int index)                      | Clear the hardware breakpoint. (Index must be 0-3)  |
| ErrorCode resetHardwareBreakpoints(int[] indexes)                 | Clear the hardware breakpoints. (Index must be 0-3) |
| ErrorCode setHardwareBreakpoint(HardwareBreakpoint breakpoint)    | Set the hardware breakpoint.                        |
| void setHardwareBreakpoints(List<HardwareBreakpoint> breakpoints) | Set the hardware breakpoints.                       |

## Examples

### Sending a PS4 notification
```
API.IP = "192.168.0.31"
if (API.connect())
{
  API.notify("Hello world!");
}
```

### Writing / Reading data to a game
```
API.ErrorCode error;
API.IP = "192.168.0.31"
if (API.connect() && API.attachEboot())
{
  int value = API.read<int>(0x400000, out error); // Read 1 Int
  int[] values = API.read<int>(0x400000, 3, out error); // Read 3 Ints
  string message = API.read<string>(0x400000, out error); // Read string
  
  API.write<string>(0x400000, "Hello world!"); // Write string
  API.write<float>(0x400000, new float[] { 1.0f, 0.5f, 2.5f }); // Write float array
}
```
