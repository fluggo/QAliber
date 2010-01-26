// guids.h: definitions of GUIDs/IIDs/CLSIDs used in this VsPackage

/*
Do not use #pragma once, as this file needs to be included twice.  Once to declare the externs
for the GUIDs, and again right after including initguid.h to actually define the GUIDs.
*/


// guidPersistanceSlot ID for the Tool Window
// { c1447f67-c464-4053-b6b3-19684aeadf62 }
#define guidUITestingPackagePersistenceSlot { 0xC1447F67, 0xC464, 0x4053, { 0xB6, 0xB3, 0x19, 0x68, 0x4A, 0xEA, 0xDF, 0x62 } }
#ifdef DEFINE_GUID
DEFINE_GUID(CLSID_guidPersistanceSlot, 
0xC1447F67, 0xC464, 0x4053, 0xB6, 0xB3, 0x19, 0x68, 0x4A, 0xEA, 0xDF, 0x62 );
#endif


// package guid
// { 118c6e7d-352f-4a92-8113-eb60385f959c }
#define guidUITestingPackagePkg { 0x118C6E7D, 0x352F, 0x4A92, { 0x81, 0x13, 0xEB, 0x60, 0x38, 0x5F, 0x95, 0x9C } }
#ifdef DEFINE_GUID
DEFINE_GUID(CLSID_UITestingPackage,
0x118C6E7D, 0x352F, 0x4A92, 0x81, 0x13, 0xEB, 0x60, 0x38, 0x5F, 0x95, 0x9C );
#endif

// Command set guid for our commands (used with IOleCommandTarget)
// { 9ab4a099-b7fb-4a7f-9395-27fdfe62991c }
#define guidUITestingPackageCmdSet { 0x9AB4A099, 0xB7FB, 0x4A7F, { 0x93, 0x95, 0x27, 0xFD, 0xFE, 0x62, 0x99, 0x1C } }
#ifdef DEFINE_GUID
DEFINE_GUID(CLSID_UITestingPackageCmdSet, 
0x9AB4A099, 0xB7FB, 0x4A7F, 0x93, 0x95, 0x27, 0xFD, 0xFE, 0x62, 0x99, 0x1C );
#endif


