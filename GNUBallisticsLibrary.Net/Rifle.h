#pragma once

using namespace System;
using namespace Sharp::Ballistics::Abstractions;
using namespace UnitsNet;

#define ZERO_RANGE 100
public ref class Rifle {
private:
	RifleInfo^ rifleInfo;
	ScopeInfo^ scopeInfo;
	AmmoInfo^ ammoInfo;

	double zeroAngle;

public:
	Rifle(RifleInfo^ rifleInfo, ScopeInfo^ scopeInfo, AmmoInfo^ ammoInfo)
	{
		if (rifleInfo == nullptr)
			throw gcnew System::ArgumentNullException("rifleInfo");

		if (rifleInfo->ZeroingConditions == nullptr)
			throw gcnew System::ArgumentNullException("rifleInfo.ZeroingConditions");

		if (scopeInfo == nullptr)
			throw gcnew System::ArgumentNullException("scopeInfo");
		if (ammoInfo == nullptr)
			throw gcnew System::ArgumentNullException("ammoInfo");

		this->rifleInfo = rifleInfo;
		this->scopeInfo = scopeInfo;
		this->ammoInfo = ammoInfo;

		zeroAngle = ZeroAngle((int)ammoInfo->DragFunction,
			AtmCorrect(ammoInfo->BC,
				rifleInfo->ZeroingConditions->Altitude.Feet,
				rifleInfo->ZeroingConditions->Barometer.Psi * 2.03602, //convert to Hg
				rifleInfo->ZeroingConditions->Temperature.DegreesFahrenheit,
				rifleInfo->ZeroingConditions->RelativeHumidity),
			ammoInfo->MuzzleVelocity.FeetPerSecond,
			scopeInfo->Height.Inches,
			scopeInfo->ZeroDistance.Yards, 0);
	}

	property String^ Name
	{
		String^ get()
		{
			return rifleInfo->Name;
		}
	}

	ShotInfo^ SolveShot(
		double shootingAngle,
		Speed windSpeed,
		double windAngle,
		Length range,
		AtmosphericInfo^ atmInfo);
};
