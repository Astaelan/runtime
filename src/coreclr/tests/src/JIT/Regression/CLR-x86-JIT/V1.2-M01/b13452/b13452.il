// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

.assembly extern mscorlib{}
.assembly b13452{}
.module b13452.exe

.class public auto ansi beforefieldinit test extends [mscorlib]System.Object
{
  .method public static void f() cil managed
  {
	ldsfld     float64 [mscorlib]System.Math::E
	pop
	ret
  }
  .method public static int32 Main()
  {
	.entrypoint
	.try
	{
	call void test::f()
	leave FAIL
	}
	catch [mscorlib]System.MissingFieldException
	{
	pop
	leave PASS
	}

	FAIL:
	ldc.i4.1
	ret

	PASS:
	ldc.i4 100
	ret
  }

}